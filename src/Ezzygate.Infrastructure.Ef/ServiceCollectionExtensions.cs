using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Ef.Context;

namespace Ezzygate.Infrastructure.Ef;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEzzygateDbContext(this IServiceCollection services)
    {
        services.AddScoped<IDbContextFactory<EzzygateDbContext>, EzzygateDbContextFactory>();

        services.AddScoped<EzzygateDbContext>(sp =>
        {
            var factory = sp.GetRequiredService<IDbContextFactory<EzzygateDbContext>>();
            return factory.CreateDbContext();
        });

        return services;
    }

    public static IServiceCollection AddErrorsDbContext(this IServiceCollection services)
    {
        services.AddScoped<IDbContextFactory<ErrorsDbContext>, ErrorsDbContextFactory>();

        services.AddScoped<ErrorsDbContext>(sp =>
        {
            var factory = sp.GetRequiredService<IDbContextFactory<ErrorsDbContext>>();
            return factory.CreateDbContext();
        });

        return services;
    }
}