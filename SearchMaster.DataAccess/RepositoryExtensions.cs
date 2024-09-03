using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchMaster.DataAccess.Repositories;

namespace SearchMaster.DataAccess
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SearchMasterDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(SearchMasterDbContext)));
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<ICodeRepository, CodeRepository>();

            return services;
        }
    }
}
