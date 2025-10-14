using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Integrations.Abstractions;
using Ezzygate.Integrations.Services;

namespace Ezzygate.Integrations.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationProvider, IntegrationProvider>();
        services.AddScoped<ICreditCardIntegrationProcessor, CreditCardIntegrationProcessor>();

        services.AddScoped<ICreditCardIntegration, MockCreditCardIntegration>();

        return services;
    }

    public static IServiceCollection AddCreditCardIntegration<TService>(this IServiceCollection services)
        where TService : class, ICreditCardIntegration
    {
        services.AddScoped<ICreditCardIntegration, TService>();
        return services;
    }

    public static IServiceCollection AddIntegration<TInterface, TService>(this IServiceCollection services)
        where TInterface : class, IIntegration
        where TService : class, TInterface
    {
        services.AddScoped<TInterface, TService>();
        return services;
    }
}