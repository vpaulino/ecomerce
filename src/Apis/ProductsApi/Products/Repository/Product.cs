namespace ProductsApi.Products.Repository
{
    public class Product
    {
        public Product(long id, string name, string description, string category, int? rank)
            : this(name, description, category, rank)
        {
            Id = id;
        }

        public Product(string name, string description, string category, int? rank)
        {

            Name = name;
            Description = description;
            Category = category;
            Rank = rank;
            Created = DateTime.UtcNow;
        }

        public long Id { get; }
        public string? Name { get; }
        public string? Description { get; }
        public string? Category { get; }
        public int? Rank { get; }
        public DateTime? Created { get; }
    }
}
