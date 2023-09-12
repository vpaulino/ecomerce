using Microsoft.AspNetCore.Mvc;
using ProductsApi.Products.Repository;
using ProductsApi.Products.SetupDb;

namespace ProductsApi.Products.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly SqlServerProductsRepository productsRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly InsertProductsService productsService;

        public ProductsController(ILogger<ProductsController> logger, SqlServerProductsRepository productsRepository, InsertProductsService productsService)
        {
            this.productsRepository = productsRepository;
            _logger = logger;
            this.productsService = productsService;
        }

        [HttpPost(Name = "setup")]
        public async Task<IActionResult> AddProductAsync(CancellationToken ctoken)
        {
            await productsService.ExecuteAsync(ctoken);
            return Ok();
        }

        [ResponseCache(Duration =60)]
        [HttpGet("count", Name = "GetProductsCount")]
        public async Task<IActionResult> GetProductsCount(CancellationToken ctoken)
        {
            long countProducts = await productsRepository.GetProductsCount(ctoken);
            return Ok(new { TotalRecords = countProducts, Date = DateTime.UtcNow });
        }


        [HttpGet(Name = "GetAllProducts")]
        public IActionResult GetAllProducts(int lastProductId, int take, CancellationToken ctoken)
        {
            IAsyncEnumerable<Product> result =  productsRepository.GetProducts(lastProductId, take, ctoken);
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductAsync(long id, CancellationToken ctoken)
        {
            Product productFound = await productsRepository.GetProductAsync(id, ctoken);
            return Ok(productFound);
        }


        [HttpGet("search", Name = "Search")]
        public IActionResult SearchAsync(int lastProductId, int take, string? keyword, CancellationToken ctoken)
        {
            var productsFound = productsRepository.SearchProductAsync(lastProductId, take, keyword, ctoken);
            return Ok(productsFound);
        }


    }
}