using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Ef;

public static class DependencyInjection
{
    public static IServiceCollection AddDataContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EzzygateDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}