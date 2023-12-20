
namespace API_Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IQueryable<Category> Categories = _service.GetAll();
            if (Categories == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Category Category = await _service.GetById(id);
            if (Category == null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK, Category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto createCategoryDto)
        {
            Category category = await _service.Create(createCategoryDto);

            return StatusCode(StatusCodes.Status201Created,category);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm]CreateCategoryDto createCategoryDto)
        {
            Category category = await _service.Update(id, createCategoryDto);

            return StatusCode(StatusCodes.Status200OK, category);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _service.Delete(id);

            return StatusCode(StatusCodes.Status200OK,category);
        }
    }
}
