using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_App_Data;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet("random")]
        public IActionResult GetRandomProducts()
        {
            var random = new Random();
            var products = new List<Product>();

            for (int i = 0; i < 5; i++)  
            {
                products.Add(new Product
                {
                    ProductCode = $"Code-{random.Next(1, 5)}",
                    ProductName = $"Product-{i.ToString()}",
                    UnitPrice = random.Next(10, 500),
                    Quantity = random.Next(1, 100)
                });
            }

            return Ok(products);
        }
    }
}
