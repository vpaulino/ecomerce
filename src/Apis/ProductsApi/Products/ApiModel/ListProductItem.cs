namespace ProductsApi.Products.ApiModel
{
    public record ListProductItem(long ProductId, string Name, string Description, string Category, decimal Price, int Rank, Dictionary<string, string> Specifications);
}
