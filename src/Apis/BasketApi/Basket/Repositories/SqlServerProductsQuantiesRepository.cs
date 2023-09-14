using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Linq.Expressions;
using System.Collections.Generic;
using BasketApi.Basket.Repositories;

namespace ProductsApi.Products.Repository
{
    public class SqlServerProductsQuantiesRepository
    {
        private ProductQuantityDbContext dbContext;

        private static readonly Func<ProductQuantityDbContext, long?,long?, int, IAsyncEnumerable<ProductQuantity>> GetProductsWPagination = EF.CompileAsyncQuery((ProductQuantityDbContext _dbContext, long? ownerId, long? productId, int take) => _dbContext.ProductQuantity.AsNoTracking().OrderBy(product => product.ProductId).Where(product => product.OwnerId == ownerId && product.ProductId > productId).Take(take));
        
        public SqlServerProductsQuantiesRepository(ProductQuantityDbContext dbcontext)
        {
            dbContext = dbcontext;
        }


        public async Task SetProductQuantityAsync(ProductQuantity product, CancellationToken token)
        {
            
                Func<ProductQuantity, bool> getProductByComposedKey = prodQuantity => prodQuantity.ProductId == product.ProductId && prodQuantity.OwnerId == product.OwnerId;

                var found = await dbContext.ProductQuantity.FirstOrDefaultAsync(prodQuantity => prodQuantity.ProductId == product.ProductId && prodQuantity.OwnerId == product.OwnerId);


                switch (product.Quantity)
                {
                    case >= 0 when found is null: 
                        {
                            dbContext.Add<ProductQuantity>(product);
                            break;
                        }
                    case >= 0 when found is not null: 
                        {
                            found.Quantity = product.Quantity;
                            found.Updated = DateTime.UtcNow;
                            break;
                        }
                    case 0 when found is not null: {

                            dbContext.Remove<ProductQuantity>(found);
                            break;
                        }
                case < 0: 
                    {
                        throw new InvalidOperationException("Products cannot have quantities < 0");
                    }
                }

                await dbContext.SaveChangesAsync();
                return;
        }

        
        public  IAsyncEnumerable<ProductQuantity> GetQuantities(long ownerId,long lastProductId, int take, CancellationToken token)
        {
            var results = GetProductsWPagination(dbContext, ownerId, lastProductId, take);
            return results;
        }
           
    }
}
