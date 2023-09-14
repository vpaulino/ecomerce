using BasketApi.Basket.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Products.Repository
{
    public class ProductQuantityDbContext : DbContext
    {

        public DbSet<ProductQuantity> ProductQuantity { get; set; }
        public ProductQuantityDbContext(DbContextOptions<ProductQuantityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductQuantity>(entity =>
            {
                entity.HasKey(p => new { p.OwnerId, p.ProductId}).IsClustered(false);
                entity.Property(p => p.Quantity);
                entity.Property(p => p.Updated);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}
