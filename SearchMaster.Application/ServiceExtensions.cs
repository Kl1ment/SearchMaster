using Microsoft.Extensions.DependencyInjection;
using SearchMaster.Application.Services;

namespace SearchMaster.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsernameService, UsernameService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IWorkerService, WorkerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ICodeService, CodeService>();

            return services;
        }
    }
}
