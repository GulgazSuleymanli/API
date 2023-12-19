using API_Test.DAL;
using API_Test.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Category> Categories = await _context.Categories.ToListAsync();
            if (Categories == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _context.Categories.FirstOrDefaultAsync(c=>c.Id==id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if(category==null) return StatusCode(StatusCodes.Status404NotFound);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created,category);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);
            Category.Name = name;
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, Category);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Categories.Remove(Category);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, Category);
        }
    }
}
