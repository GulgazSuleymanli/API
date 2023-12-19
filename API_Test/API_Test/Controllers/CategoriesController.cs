using API_Test.DAL;
using API_Test.DTOs.CategoryDtos;
using API_Test.Entities;
using API_Test.Repositories.Interfaces;
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
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context,IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Category> Categories = await _repository.GetAll();
            if (Categories == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _repository.GetByIdAsync(id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto createCategoryDto)
        {
            if(createCategoryDto == null) return StatusCode(StatusCodes.Status404NotFound);
            Category category = new Category()
            {
                Name = createCategoryDto.Name,
            };
            await _repository.Create(category);
            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created,category);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _repository.GetByIdAsync(id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);
            Category.Name = name;
            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, Category);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _repository.GetByIdAsync(id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);
            _repository.Delete(Category);
            await _repository.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, Category);
        }
    }
}
