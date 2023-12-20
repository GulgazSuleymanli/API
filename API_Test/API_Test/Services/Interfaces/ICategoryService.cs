namespace API_Test.Services.Interfaces
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();

        Task<Category> GetById(int id);

        Task<Category> Create(CreateCategoryDto createCategoryDto);

        Task<Category> Update(int id, CreateCategoryDto createCategoryDto);

        Task<Category> Delete(int id);
    }
}
