using WebSite.Models;

namespace WebSite.Services
{
    public class ProductsService
    {
        private HttpClient _productsApiHttpClient;
        public ProductsService(HttpClient client)
        {
            _productsApiHttpClient = client;

        }

        public async Task<IEnumerable<Product>> GetAllProducts(long lastProductId, int pageSize, CancellationToken ctoken) {

            try
            {
                var results = await _productsApiHttpClient.GetFromJsonAsync<IEnumerable<Product>>($"/api/v1/Products?lastProductId={lastProductId}&take={pageSize}", ctoken);
                return results;
            }
            catch (Exception)
            {
                return Enumerable.Empty<Product>();
            }
            
        }

        public async Task<long> GetProductsCount(CancellationToken ctoken)
        {
                var productCount = await _productsApiHttpClient.GetFromJsonAsync<ProductCount>($"/api/v1/Products/count", ctoken);
                return productCount.TotalRecords;
        }

        internal async Task<IEnumerable<Product>> GetAllProductsByKeyword(long lastProductId, int pageSize, string searchText, CancellationToken ctoken)
        {
            try
            {
                var results = await _productsApiHttpClient.GetFromJsonAsync<IEnumerable<Product>>($"/api/v1/products/search?lastProductId={lastProductId}&take={pageSize}&keyword={searchText}", ctoken);
                return results;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Product>();
                throw;
            }
         
        }
    }
}
