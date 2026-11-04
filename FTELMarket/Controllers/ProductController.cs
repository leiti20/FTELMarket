using FTELMarket.Infrastructure;
using FTELMarket.Infrastructure.Entities;
using FTELMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly FtelMarketDbContext _db;

        public ProductsController(FtelMarketDbContext db)
        {
            _db = db;
        }

        private static ProductResponse ToResponse(Product p) =>
            new(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId, p.CreatedAt, p.Category.Name);

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var products = await _db.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .OrderBy(p => p.Id)
                .ToListAsync();

            return Ok(products.Select(ToResponse).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest request)
        {
            var category = await _db.Categories.FindAsync(request.CategoryId);
            if (category == null)
                return BadRequest(new { message = "Catégorie inconnue." });

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                Category = category,
                CreatedAt = DateTime.UtcNow
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok(ToResponse(product));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, ProductRequest request)
        {
            var product = await _db.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            var category = await _db.Categories.FindAsync(request.CategoryId);
            if (category == null)
                return BadRequest(new { message = "Catégorie inconnue." });

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            product.Category = category;

            await _db.SaveChangesAsync();

            return Ok(ToResponse(product));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}