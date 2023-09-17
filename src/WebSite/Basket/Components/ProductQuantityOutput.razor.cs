using Microsoft.AspNetCore.Components;

namespace WebSite.Basket.Components
{
    public partial class ProductQuantityOutput
    {
        [Parameter]
        public long BasketItemCount { get; set; }

        
        public ProductQuantityOutput()
        {
            
        }

        protected override async Task OnInitializedAsync()
        {
            long count = await this.basketServiceClient.GetItemsCountAsync(1, CancellationToken.None);
            this.BasketItemCount = count;
            await base.OnInitializedAsync();
          
        }

        internal async Task UpdateQuantity(ProductQuantityEventArgs args)
        {
            long count = await this.basketServiceClient.GetItemsCountAsync(1, CancellationToken.None);
            this.BasketItemCount = count;
            await InvokeAsync(() => StateHasChanged());

        }
 
    }
}
