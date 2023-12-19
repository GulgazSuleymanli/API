using API_Test.DAL;
using API_Test.Entities;
using API_Test.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API_Test.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext? _context;

        public Repository(AppDbContext? context)
        {
            _context = context;
        }

        public async Task<IQueryable<Category>> GetAll(Expression<Func<Category, bool>>? func = null, params string[]? includes)
        {
            IQueryable<Category> query = _context.Categories.AsNoTracking();

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

            return query;
        }

        public Task<Category> GetByIdAsync(int id, params string[]? includes)
        {
            IQueryable<Category> query = _context.Categories;

            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }

            return query.FirstOrDefaultAsync(c=>c.Id==id);
        }

        public async Task Create(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
