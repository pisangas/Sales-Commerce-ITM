using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CategoriesController : ControllerBase
    {        
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null) { return NotFound(); }
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
