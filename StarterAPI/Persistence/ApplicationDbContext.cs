using Microsoft.EntityFrameworkCore;
using StarterAPI.Entities;
using StarterAPI.Interfaces;
using System.Reflection;

namespace StarterAPI.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base (options)
        { }

        public DbSet<Student> Students => Set<Student>();

        public DbSet<Class> Classes => Set<Class>();

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


    }

}
