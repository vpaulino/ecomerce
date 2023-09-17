namespace WebSite.Basket.Models
{
    public class ProductQuantity
    {
        public long OwnerId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime Updated { get; set; }
    }
}
