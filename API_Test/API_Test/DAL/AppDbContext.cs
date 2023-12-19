using API_Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Test.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
