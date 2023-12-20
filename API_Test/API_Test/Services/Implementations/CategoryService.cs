
namespace API_Test.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public IQueryable<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Category> GetById(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<Category> Create(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null) throw new Exception();
            Category category = new Category()
            {
                Name = createCategoryDto.Name,
            };
            await _repository.Create(category);
            await _repository.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(int id,CreateCategoryDto categoryDto)
        {
            if (id <= 0) throw new Exception();
            Category Category = await _repository.GetByIdAsync(id);

            if (Category == null) throw new Exception();
            Category.Name = categoryDto.Name;

            _repository.Update(Category);
            await _repository.SaveChangesAsync();

            return Category;
        }

        public async Task<Category> Delete(int id)
        {
            if (id <= 0) throw new Exception();
            Category Category = await _repository.GetByIdAsync(id);
            if (Category == null) throw new Exception();

            _repository.Delete(Category);
            await _repository.SaveChangesAsync();

            return Category;
        }
    }
}
