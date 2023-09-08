using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace ProductsApi.Products.Repository
{
    public class SqlServerProductsRepository
    {
        private ProductsDbContext dbContext;


        private static readonly Func<ProductsDbContext,long, int, IQueryable<Product>> GetProductsWPagination = EF.CompileQuery<ProductsDbContext,long, int, IQueryable<Product>>((_dbContext, lastProductId, take) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId).Take(take));

        private static readonly Func<ProductsDbContext, long, int, int, IQueryable<Product>> GetProductsByRankWPagination = EF.CompileQuery<ProductsDbContext, long, int,int, IQueryable<Product>>((_dbContext, lastProductId, take, rank) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId && product.Rank == rank).Take(take));

        private static readonly Func<ProductsDbContext, long, int, string, IQueryable<Product>> GetProductsByKeywordWPagination = EF.CompileQuery<ProductsDbContext, long, int, string, IQueryable<Product>>((_dbContext, lastProductId, take, keyword) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId && (product.Name.Contains(keyword) || product.Description.Contains(keyword))).Take(take));

        public SqlServerProductsRepository(ProductsDbContext dbcontext)
        {
            dbContext = dbcontext;
        }


        public async Task AddProductAsync(Product product, CancellationToken token)
        {
            await dbContext.AddAsync(product, token);
            await dbContext.SaveChangesAsync();
        }

        
        public  IAsyncEnumerable<Product> GetProducts(long lastProductId, int take, CancellationToken token)
        {
            var queryable = GetProductsWPagination(this.dbContext, lastProductId, take);
            return queryable.AsAsyncEnumerable();
        }

        public  IAsyncEnumerable<Product> GetProductsRankedAsync(long lastProductId, int take, int rank, CancellationToken token)
        {
            var queryable = GetProductsByRankWPagination(this.dbContext, lastProductId, take, rank);
            return queryable.AsAsyncEnumerable();
        }

        public async Task<Product> GetProductAsync(long id, CancellationToken ctoken)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Id == id).FirstOrDefaultAsync(ctoken);
        }

        public IAsyncEnumerable<Product> SearchProductAsync(long lastProductId, int take, string keyword, CancellationToken ctoken)
        {
            var queryable = GetProductsByKeywordWPagination(this.dbContext, lastProductId, take, keyword);
            return queryable.AsAsyncEnumerable();
        }
    }
}
