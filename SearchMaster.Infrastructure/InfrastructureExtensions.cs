using Microsoft.Extensions.DependencyInjection;

namespace SearchMaster.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<IHasher, Hasher>();
            service.AddTransient<IJwtProvider, JwtProvider>();

            return service;
        }
    }
}
