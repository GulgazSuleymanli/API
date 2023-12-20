
namespace API_Test.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext? _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext? context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? func = null, Expression<Func<T, object>>? orderby = null, bool isDisting = false, params string[] includes)
        {
            IQueryable<T> query = _table.AsNoTracking();

            if(func != null)
            {
                query = query.Where(func);
            }

            if(includes != null)
            {
                for(int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            if(orderby != null)
            {
                query = isDisting? query.OrderByDescending(orderby) : query.OrderBy(orderby);
            }

            return query;
        }

        public Task<T> GetByIdAsync(int id, params string[] includes)
        {
            IQueryable<T> query = _table.AsNoTracking();

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task Create(T entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
