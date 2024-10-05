using Microsoft.AspNetCore.Mvc;
using Web_App_Data.Service;
using Web_App_Data;
using Microsoft.AspNetCore.Authorization;

namespace Web_App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly ProductService _productService;

        public HomeController(IHttpClientFactory httpClientFactory, ProductService productService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _productService = productService;
        }

        public async Task<IActionResult> FetchAndSaveProducts()
        {
            var response = await _httpClient.GetAsync("/api/Products/random");

            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<Product>>();
                await _productService.AddProductsAsync(products);
                return RedirectToAction("ProductList");
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Json(product);
     
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id ,[FromBody] Product product)
        {
            product.Id = id;
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz veri");
            }
            await _productService.UpdateProductAsync(product);

            return Json(new { success = true, message = "Ürün başarıyla güncellendi" });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);

                return Json(product);
                //return Json(new
                //{
                //    success = true,
                //    productId = product.Id,
                //    productCode = product.ProductCode,
                //    productName = product.ProductName,
                //    quantity = product.Quantity,
                //    unitPrice = product.UnitPrice
                //});
            }
            return Json(new { success = false, message = "Geçersiz veri" });
        }
    }
}
