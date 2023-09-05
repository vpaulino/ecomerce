namespace ProductsApi.Repository
{
    public class Product
    {
        public Product(long id, string name, string description, string category, int? rank)
            :this(name, description, category,rank)
        {
            this.Id = id;
        }

        public Product(string name, string description, string category, int? rank)
        {
         
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Rank = rank;
            this.Created = DateTime.UtcNow;
        }

        public long Id { get; }
        public string? Name { get; }
        public string? Description { get; }
        public string? Category { get; }
        public int? Rank { get; }
        public DateTime? Created { get; }
    }
}
