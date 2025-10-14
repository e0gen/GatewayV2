using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Ef;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddEzzygateDbContext(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContextFactory<EzzygateDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
            if (environment.IsDevelopment())
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 00)));
            else
                options.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddErrorsDbContext(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContext<ErrorsDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ErrorsConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'ErrorsConnection' not found in configuration.");
            if (environment.IsDevelopment())
                options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 4, 00)));
            else
                options.UseSqlServer(connectionString);
        });

        return services;
    }
}