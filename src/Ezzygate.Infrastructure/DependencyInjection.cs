using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ezzygate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContexts
        services.AddDbContext<Data.EzzygateDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
        services.AddDbContext<Data.ReportsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ReportsConnection")));
            
        services.AddDbContext<Data.ErrorsDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ErrorsConnection")));

        // Add repositories and services (to be implemented)
        // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
} 