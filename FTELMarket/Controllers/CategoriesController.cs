using Microsoft.AspNetCore.Mvc;

namespace FTELMarket.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        public static readonly List<Category> Categories = new()
        {
            new Category { Id = 1, Name = "PlayStation 5" },
            new Category { Id = 2, Name = "Nintendo Switch" },
            new Category { Id = 3, Name = "Xbox Series X" },
            new Category { Id = 4, Name = "PC" }
        };

        [HttpGet]
        public List<Category> GetCategories() => Categories;
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

