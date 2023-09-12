using Microsoft.AspNetCore.Components;

namespace WebSite.Basket.Components
{
    public partial class ProductQuantityInput
    {
        [Parameter]
        public int Quantity { get; set; }

        [Parameter]
        public long ProductId { get; set; }

        public ProductQuantityInput()
        {
            
        }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private void IncreaseQuantity()
        {
            Quantity++;
        }

        private void DecreaseQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
        }
    }
}
