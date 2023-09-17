using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using WebSite.Basket.Services;

namespace WebSite.Basket.Components
{
    public partial class ProductQuantityInput
    {
        [Parameter]
        public int Quantity { get; set; }

        [Parameter]
        public int CurrentQuantity { get; set; }

        [Parameter]
        public long ProductId { get; set; }

        [Parameter]
        public EventCallback<ProductQuantityEventArgs> OnQuantityUpdated { get; set; }



        public ProductQuantityInput()
        {
            
        }

        protected override Task OnInitializedAsync()
        {
          
            return base.OnInitializedAsync();
            
            
        }

        private async Task PersistQuantity() 
        {
            this.CurrentQuantity = Quantity;
            await basketServiceClient.SetItemToBasketAsync(new Models.ProductQuantityApiModel(1, this.ProductId, Quantity), CancellationToken.None);
            await OnQuantityUpdated.InvokeAsync(new ProductQuantityEventArgs() { ProductId = this.ProductId, Quantity = Quantity });
            await InvokeAsync(()=>StateHasChanged());
        }
    }
}
