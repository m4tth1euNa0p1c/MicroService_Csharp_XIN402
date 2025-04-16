using Microsoft.AspNetCore.Mvc;
using ms_product_service.Models;
using ms_product_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ms_product_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get() =>
            Ok(await _productService.GetAsync());

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Product product)
        {
            if (product == null)
                return BadRequest("Product data is null.");
            await _productService.CreateAsync(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> Update(string id, Product updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest("Product data is null.");
            var existingProduct = await _productService.GetAsync(id);
            if (existingProduct == null)
                return NotFound();
            updatedProduct.Id = existingProduct.Id;
            await _productService.UpdateAsync(id, updatedProduct);
            return NoContent();
        }

        [HttpPatch("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Patch document is null.");
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();
            patchDoc.ApplyTo(product, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _productService.UpdateAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();
            await _productService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Search(
            [FromQuery] string? q,
            [FromQuery] string? cat,
            [FromQuery] List<string>? tags)
        {
            List<string> parsedCategories = new List<string>();
            if (!string.IsNullOrWhiteSpace(cat))
            {
                parsedCategories = cat.Split(',')
                                      .Select(x => x.Trim().ToLowerInvariant())
                                      .Where(x => !string.IsNullOrWhiteSpace(x))
                                      .ToList();
            }
            
            string? searchText = (string.IsNullOrWhiteSpace(q) || q.Length < 3 ||
                                  string.Equals(q, "all", StringComparison.OrdinalIgnoreCase))
                                  ? null
                                  : q;
            
            var filter = new ProductFilter
            {
                NomProduit = searchText,
                Categories = parsedCategories,
                Tags = tags
            };
            
            var products = await _productService.GetFilteredAsync(filter);
            return Ok(products);
        }

        [HttpGet("trending")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetTrending() =>
            Ok(await _productService.GetTrendingAsync());

        [HttpGet("new-arrivals")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetNewArrivals() =>
            Ok(await _productService.GetNewArrivalsAsync());

        [HttpGet("featured")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetFeatured() =>
            Ok(await _productService.GetFeaturedAsync());

        [HttpGet("related/{id:length(24)}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetRelated(string id) =>
            Ok(await _productService.GetRelatedAsync(id));

        [HttpGet("summary")]
        [AllowAnonymous]
        public async Task<ActionResult> GetSummary() =>
            Ok(await _productService.GetSummaryAsync());

        [HttpPost("bulk-import")]
        [Authorize]
        public async Task<IActionResult> BulkImport([FromBody] List<Product> products)
        {
            if (products == null || !products.Any())
                return BadRequest("No products provided for import.");
            await _productService.BulkImportAsync(products);
            return Ok(new { message = "Bulk import successful", count = products.Count });
        }

        [HttpPut("{id:length(24)}/status")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] StatusUpdateModel statusUpdate)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();
            product.EstDisponible = statusUpdate.EstDisponible;
            await _productService.UpdateAsync(id, product);
            return NoContent();
        }
    }
}
