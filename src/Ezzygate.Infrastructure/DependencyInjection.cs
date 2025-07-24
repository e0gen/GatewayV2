using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Ef;

namespace Ezzygate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataContextInfrastructure(configuration);

        // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}