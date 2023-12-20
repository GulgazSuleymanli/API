
namespace API_Test.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T,bool>>? func=null, Expression<Func<T, object>>? orderby = null, bool isDisting=false, params string[] includes);

        Task<T> GetByIdAsync(int id, params string[] includes);

        Task Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChangesAsync();

    }
}
