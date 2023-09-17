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


    [HttpPut("item")]
    public async Task<IActionResult> AddItemToBasketAsync(ProductQuantityApiModel productQuantity, CancellationToken token)
    {
        try
        {
            
            DateTime updated = DateTime.UtcNow;
            await repository.UpdateBasketAsync(new ProductQuantity() { OwnerId = productQuantity.OwnerId, ProductId = productQuantity.ProductId, Quantity = productQuantity.Quantity, Updated = updated }, token);
            await cache.EvictByTagAsync("tag-baskets", token);
            return Ok(new { ProductId = productQuantity.ProductId, Updated = updated });
        }
        catch (InvalidOperationException ex)
        {

            return BadRequest(new { OwnerId = productQuantity.OwnerId, ProductId = productQuantity.ProductId, ErrorMessage = ex.Message });
        }

    }

    [HttpGet("{ownerId}/items")]
    [OutputCache( PolicyName = "basketCached", VaryByRouteValueNames = new string[] { "ownerId"}, VaryByQueryKeys = new string[] { "lastProductId", "take" })]
    public async Task<IActionResult> GetItemsFromBasketAsync(long ownerId,long lastProductId, int take, CancellationToken token)
    {
        
            var results = repository.GetBasketItems(ownerId, lastProductId, take, token);
            return Ok(results);
    }

    [HttpGet("{ownerId}/count")]
    [OutputCache(PolicyName = "basketCountCached", VaryByRouteValueNames = new string[] { "ownerId" })]
    public IActionResult GetItemsCountAsync(long ownerId,  CancellationToken token)
    {
        var count = repository.GetBasketItemsCount(ownerId, token);
        return Ok(new { OwnerId = ownerId, Count = count });
    }


}