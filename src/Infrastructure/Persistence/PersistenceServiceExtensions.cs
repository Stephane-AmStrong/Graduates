using Application.Interfaces;
using Domain.Entities;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using Persistence.Repositories;

namespace Persistence
{
    public static class PersistenceServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer
                (
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
            );


            #region Repositories
            services.AddScoped<ISortHelper<Diploma>, SortHelper<Diploma>>();
            services.AddScoped<ISortHelper<Graduate>, SortHelper<Graduate>>();
            services.AddScoped<ISortHelper<Student>, SortHelper<Student>>();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            #endregion
        }
    }
}
