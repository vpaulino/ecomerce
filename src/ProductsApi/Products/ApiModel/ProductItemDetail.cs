namespace ProductsApi.Products.ApiModel
{
    public record ProductItemDtail : ListProductItem
    {
        public ProductItemDtail(long ProductId, string Name, string Description, string Category, decimal Price, int Rank, Dictionary<string, string> Specifications) : base(ProductId, Name, Description, Category, Price, Rank, Specifications)
        {
            this.Specifications = new Dictionary<string, string>();
        }
    }
}
