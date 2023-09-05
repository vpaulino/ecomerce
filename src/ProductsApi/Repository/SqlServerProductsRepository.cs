using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ProductsApi.Repository
{
    public class SqlServerProductsRepository
    {
        private ProductsDbContext dbContext;

        public SqlServerProductsRepository(ProductsDbContext dbcontext)
        {
            this.dbContext = dbcontext;
        }


        public async Task AddProductAsync(Product product, CancellationToken token) 
        {
            await this.dbContext.AddAsync(product, token);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int take, int skip, CancellationToken token) 
        {
            return await this.dbContext.Product.AsNoTracking().Take(take).Skip(skip).ToListAsync(token);
        }

        public async Task<IEnumerable<Product>> GetProductsRankedAsync(int take, int skip,int rank, CancellationToken token)
        {
            return await this.dbContext.Product.AsNoTracking().Where(product => product.Rank == rank).Take(take).Skip(skip).ToListAsync(token);
        }

        public async Task<Product> GetProductAsync(long id, CancellationToken ctoken)
        {
            return await dbContext.Product.AsNoTracking().Where(product => product.Id == id).FirstOrDefaultAsync(ctoken);
        }

        public async Task<IEnumerable<Product>> SearchProductAsync(int take, int skip, string keyword, CancellationToken ctoken)
        {
            return await this.dbContext.Product.AsNoTracking().Where(product => product.Name.Contains(keyword) || product.Description.Contains(keyword)).Take(take).Skip(skip).ToListAsync(ctoken);
        }
    }
}
