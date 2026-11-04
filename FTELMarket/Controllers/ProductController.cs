using Microsoft.AspNetCore.Mvc;


namespace FTELMarket.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "The Last of Us Part II", Description = "Boîte et notice, disque sans rayure.", Price = 19.9m, Stock = 3, CategoryId = 1, CreatedAt = DateTime.Parse("2026-09-02T09:15:00Z"), CategoryName = "PlayStation 5" },
            new Product { Id = 2, Name = "Gran Turismo 7", Description = null, Price = 29.9m, Stock = 1, CategoryId = 1, CreatedAt = DateTime.Parse("2026-09-05T14:30:00Z"), CategoryName = "PlayStation 5" },
            new Product { Id = 3, Name = "The Legend of Zelda: Tears of the Kingdom", Description = "Cartouche seule.", Price = 39.9m, Stock = 2, CategoryId = 2, CreatedAt = DateTime.Parse("2026-09-08T10:00:00Z"), CategoryName = "Nintendo Switch" },
            new Product { Id = 4, Name = "Mario Kart 8 Deluxe", Description = "Complet, très bon état.", Price = 34.9m, Stock = 5, CategoryId = 2, CreatedAt = DateTime.Parse("2026-09-10T16:45:00Z"), CategoryName = "Nintendo Switch" },
            new Product { Id = 5, Name = "Halo Infinite", Description = null, Price = 14.9m, Stock = 4, CategoryId = 3, CreatedAt = DateTime.Parse("2026-09-12T11:20:00Z"), CategoryName = "Xbox Series X" },
            new Product { Id = 6, Name = "Forza Horizon 5", Description = "Édition standard.", Price = 24.9m, Stock = 0, CategoryId = 3, CreatedAt = DateTime.Parse("2026-09-15T08:05:00Z"), CategoryName = "Xbox Series X" },
            new Product { Id = 7, Name = "Baldur’s Gate 3", Description = "Clé physique, coffret collector.", Price = 44.9m, Stock = 1, CategoryId = 4, CreatedAt = DateTime.Parse("2026-09-18T17:40:00Z"), CategoryName = "PC" },
            new Product { Id = 8, Name = "Hades", Description = null, Price = 9.9m, Stock = 6, CategoryId = 4, CreatedAt = DateTime.Parse("2026-09-21T13:10:00Z"), CategoryName = "PC" }
        };

        [HttpGet]
        public List<Product> GetProducts()
        {
            return _products;
        }

        [HttpPost]
        public IActionResult CreateProduct(Product newProduct)
        {
            newProduct.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            newProduct.CreatedAt = DateTime.UtcNow;
            newProduct.CategoryName = CategoriesController.Categories
                .FirstOrDefault(c => c.Id == newProduct.CategoryId)?.Name;
            _products.Add(newProduct);
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return NotFound();

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Price = updatedProduct.Price;
            existing.Stock = updatedProduct.Stock;
            existing.CategoryId = updatedProduct.CategoryId;
            existing.CategoryName = CategoriesController.Categories
                .FirstOrDefault(c => c.Id == updatedProduct.CategoryId)?.Name;

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            _products.Remove(product);
            return NoContent();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CategoryName { get; set; }
    }
}