using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StarterAPI.Commons.Mappings;
using StarterAPI.Interfaces;
using StarterAPI.Persistence;
using StarterAPI.Services;

namespace StarterAPI.Commons.Extensions
{
    public static class ProgramExtensions
    {
        //public static 

        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {

            //Add DI for app services
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IClassService, ClassService>();

            return services;
        }

        public static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(
                options =>
                options.UseSqlite("Data Source=./students.db")
            );

            //Dependency Injection - Singleton, Scoped, Transient
            services.AddScoped<IApplicationDbContext>
                (provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {

            //https://www.kafle.io/tutorials/asp-dot-net/automapper

            var configMapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfiles());
            });

            var mapper = configMapper.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
