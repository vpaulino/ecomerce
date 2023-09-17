using Microsoft.AspNetCore.Mvc;
using WebSite.Basket.Models;

namespace WebSite.Basket.Services
{
    public class BasketServiceClient
    {
        private readonly HttpClient client;
        public BasketServiceClient(HttpClient client)
        {
            this.client = client;
        }

       public async Task SetItemToBasketAsync(ProductQuantityApiModel productQuantity, CancellationToken token) 
        {
            
           var response = await this.client.PutAsJsonAsync<ProductQuantityApiModel>("/api/v1/baskets/item", productQuantity, token);
           return;
        }

        public async Task<IEnumerable<ProductQuantity>> GetItemsFromBasketAsync(long ownerId, long lastProductId, int take, CancellationToken token) 
        {
            var response = await this.client.GetFromJsonAsync<IEnumerable<ProductQuantity>>($"/api/v1/baskets/{ownerId}/items?lastProductId={lastProductId}&take={take}", token);

            return response;
        }

        public async Task<long> GetItemsCountAsync(long ownerId, CancellationToken token)
        {
            var response = await this.client.GetFromJsonAsync<BasketCountApiModel>($"/api/v1/baskets/{ownerId}/count", token);

            return response.Count;
        }
    }
}
