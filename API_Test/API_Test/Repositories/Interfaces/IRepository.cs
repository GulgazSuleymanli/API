using API_Test.Entities;
using System.Linq.Expressions;

namespace API_Test.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAll(Expression<Func<Category,bool>>? func=null, params string[]? includes);

        Task<Category> GetByIdAsync(int id, params string[]? includes);

        Task Create(Category category);

        void Update(Category category);

        void Delete(Category category);

        Task SaveChangesAsync();

    }
}
