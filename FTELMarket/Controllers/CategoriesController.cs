using FTELMarket.Infrastructure;
using FTELMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly FtelMarketDbContext _db;

        public CategoriesController(FtelMarketDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetCategories()
        {
            var categories = await _db.Categories
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new CategoryResponse(c.Id, c.Name))
                .ToListAsync();

            return Ok(categories);
        }
    }
}