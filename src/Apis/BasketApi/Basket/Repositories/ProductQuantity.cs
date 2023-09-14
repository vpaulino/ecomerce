using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BasketApi.Basket.Repositories
{
    
    public class ProductQuantity
    {
        //public int Id { get; set; }

        public long OwnerId { get; set; }

        public long ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime Updated { get; set; }
    }
}
