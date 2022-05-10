using Microsoft.EntityFrameworkCore;
using StarterAPI.Entities;

namespace StarterAPI.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        { }

        public DbSet<Student> Students => Set<Student>();



    }

}
