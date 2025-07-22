using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ezzygate.Infrastructure.Ef;

public static class DependencyInjection
{
    public static IServiceCollection AddDataContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EzzygateDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddDbContext<ReportsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ReportsConnection")));
            
        services.AddDbContext<ErrorsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ErrorsConnection")));

        return services;
    }
} 