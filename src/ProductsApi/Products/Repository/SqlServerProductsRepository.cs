using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Linq.Expressions;

namespace ProductsApi.Products.Repository
{
    public class SqlServerProductsRepository
    {
        private ProductsDbContext dbContext;

        private static readonly Func<ProductsDbContext, long?,int, IAsyncEnumerable<Product>> GetProductsWPagination = EF.CompileAsyncQuery((ProductsDbContext _dbContext, long? lastProductId, int take) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId).Take(take));

        private static readonly Func<ProductsDbContext, long?, int, int, IAsyncEnumerable<Product>> GetProductsByRankWPagination = EF.CompileAsyncQuery((ProductsDbContext _dbContext,long? lastProductId, int take, int rank) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId && product.Rank == rank).Take(take));

        private static readonly Func<ProductsDbContext, long?, int, string, IAsyncEnumerable<Product>> GetProductsByKeywordWPagination = EF.CompileAsyncQuery((ProductsDbContext _dbContext, long? lastProductId, int take,string keyword) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId && (product.Name.Contains(keyword) || product.Description.Contains(keyword))).Take(take));

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
            var results = GetProductsWPagination(dbContext, lastProductId, take);
            return results;
        }

        public  IAsyncEnumerable<Product> GetProductsRankedAsync(long lastProductId, int take, int rank, CancellationToken token)
        {
            var results = GetProductsByRankWPagination(this.dbContext, lastProductId, take, rank);
            return results;
        }

        public async Task<Product> GetProductAsync(long id, CancellationToken ctoken)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Id == id).FirstOrDefaultAsync(ctoken);
        }

        public IAsyncEnumerable<Product> SearchProductAsync(long lastProductId, int take, string keyword, CancellationToken ctoken)
        {
            var results = GetProductsByKeywordWPagination(this.dbContext, lastProductId, take, keyword);
            return results;
        }

        public async Task<long> GetProductsCount(CancellationToken ctoken)
        {
            return await dbContext.Product.CountAsync(ctoken);
        }
    }
}
