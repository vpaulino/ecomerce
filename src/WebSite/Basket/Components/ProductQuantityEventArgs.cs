using Microsoft.AspNetCore.Components;

namespace WebSite.Basket.Components
{
    public class ProductQuantityEventArgs : EventArgs
    {

        public ProductQuantityEventArgs()
        {
            
        }
        public int Quantity { get; set; }

        public long ProductId { get; set; }
    }

    
}
