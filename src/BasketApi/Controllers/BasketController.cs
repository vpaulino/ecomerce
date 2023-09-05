using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        
        private readonly ILogger<BasketController> logger;
        
        public BasketController(ILogger<BasketController> _logger)
        {
            this.logger = _logger;    
        }


        [HttpGet(Name = "GetBasketContent")]
        public IActionResult GetBasket(int skip, int take, CancellationToken ctoken)
        {
            return Ok();
        }

        [HttpPost(Name = "CreateBasket")]
        public IActionResult AddItem(CancellationToken ctoken)
        {
            return Ok();
        }


        [HttpDelete(Name = "CreateBasket")]
        public IActionResult RemoveItem(CancellationToken ctoken)
        {
            return Ok();
        }
    }
}