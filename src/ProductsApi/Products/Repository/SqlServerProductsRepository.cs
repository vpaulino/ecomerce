using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ProductsApi.Products.Repository
{
    public class SqlServerProductsRepository
    {
        private ProductsDbContext dbContext;


        private static readonly Func<ProductsDbContext,long, int, IQueryable<Product>> GetProductsWPagination = EF.CompileQuery<ProductsDbContext,long, int, IQueryable<Product>>((_dbContext, lastProductId, take) => _dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Where(product => product.Id >= lastProductId).Take(take));

        public SqlServerProductsRepository(ProductsDbContext dbcontext)
        {
            dbContext = dbcontext;
            
            
        }


        public async Task AddProductAsync(Product product, CancellationToken token)
        {
            await dbContext.AddAsync(product, token);
            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        ///  SELECT [t].[Id], [t].[Category], [t].[Created], [t].[Description], [t].[Name], [t].[Rank]
        ////FROM(
        ////SELECT TOP(@__p_0)[p].[Id], [p].[Category], [p].[Created], [p].[Description], [p].[Name], [p].[Rank]
        ////      FROM [Product] AS [p]
        ////) AS[t]
        ////ORDER BY(SELECT 1)
        ////  OFFSET @__p_1 ROWS</remarks>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<Product>> GetProductsAsync(int take, int skip, CancellationToken token)
        //{
        //    return await dbContext.Product.AsNoTracking().OrderBy(product => product.Id).Take(take).Skip(skip).ToListAsync(token);
        //}

        public  IAsyncEnumerable<Product> GetProducts(long lastProductId, int take, CancellationToken token)
        {
            var queryable = GetProductsWPagination(this.dbContext, lastProductId, take);
            return queryable.AsAsyncEnumerable();
        }

        public async Task<IEnumerable<Product>> GetProductsRankedAsync(int take, int skip, int rank, CancellationToken token)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Rank == rank).Take(take).Skip(skip).ToListAsync(token);
        }

        public async Task<Product> GetProductAsync(long id, CancellationToken ctoken)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Id == id).FirstOrDefaultAsync(ctoken);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(int take, int skip, string keyword, CancellationToken ctoken)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Name.Contains(keyword) || product.Description.Contains(keyword)).Take(take).Skip(skip).ToListAsync(ctoken);
        }
    }
}
