using Microsoft.AspNetCore.Mvc;
using ProductsApi.ApiModel;
using ProductsApi.Repository;
using ProductsApi.SetupDb;

namespace ProductsApi.Controllers
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
            await this.productsService.ExecuteAsync(ctoken);
            return Ok();
        }


        [HttpGet(Name = "GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(int skip, int take, CancellationToken ctoken)
        {
            var result = await this.productsRepository.GetProductsAsync(take, skip, ctoken);
            return Ok(result);
        }
          
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> GetProductAsync(long id, CancellationToken ctoken)
        {
            Product productFound = await this.productsRepository.GetProductAsync(id, ctoken);
            return Ok(productFound);
        }


        [HttpGet("search", Name = "Search")]
        public async Task<IActionResult> SearchAsync(int skip, int take, string keyword, CancellationToken ctoken)
        {
            IEnumerable<Product> productsFound = await this.productsRepository.SearchProductAsync(take, skip, keyword, ctoken);
            return Ok(productsFound);
        }


    }
}