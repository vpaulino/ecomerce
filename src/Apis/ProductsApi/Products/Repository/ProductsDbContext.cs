using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Products.Repository
{
    public class ProductsDbContext : DbContext
    {

        public DbSet<Product> Product { get; set; }
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name);
                entity.Property(p => p.Description);
                entity.Property(p => p.Category);
                entity.Property(p => p.Rank);
                entity.Property(p => p.Created);
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}
