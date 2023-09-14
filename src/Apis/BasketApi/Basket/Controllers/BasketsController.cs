using BasketApi.Basket.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using ProductsApi.Products.Repository;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly SqlServerProductsQuantiesRepository repository;
    private IOutputCacheStore cache;
    public BasketsController(SqlServerProductsQuantiesRepository repository, IOutputCacheStore cache)
    {
        this.repository = repository;
        this.cache = cache;
    }


    [HttpPut("product")]
    public async Task<IActionResult> AddItemToBasketAsync(ProductQuantityApiModel productQuantity, CancellationToken token)
    {
        try
        {
            
            DateTime updated = DateTime.UtcNow;
            await repository.SetProductQuantityAsync(new ProductQuantity() { OwnerId = productQuantity.OwnerId, ProductId = productQuantity.ProductId, Quantity = productQuantity.Quantity, Updated = updated }, token);
            await cache.EvictByTagAsync("tag-baskets", token);
            return Ok(new { ProductId = productQuantity.ProductId, Updated = updated });
        }
        catch (InvalidOperationException ex)
        {

            return BadRequest(new { OwnerId = productQuantity.OwnerId, ProductId = productQuantity.ProductId, ErrorMessage = ex.Message });
        }

    }

    [HttpGet("{ownerId}")]
    [OutputCache( PolicyName = "basketCached", VaryByRouteValueNames = new string[] { "ownerId"}, VaryByQueryKeys = new string[] { "lastProductId", "take" })]
    public async Task<IActionResult> GetItemsFromBasketAsync(long ownerId,long lastProductId, int take, CancellationToken token)
    {
        
            var results = repository.GetQuantities(ownerId, lastProductId, take, token);
            return Ok(results);
    }


}