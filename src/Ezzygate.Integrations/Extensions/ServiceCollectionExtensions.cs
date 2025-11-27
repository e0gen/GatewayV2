using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Integrations.Core;
using Ezzygate.Integrations.Core.Abstractions;
using Ezzygate.Integrations.Core.Events;
using Ezzygate.Integrations.Core.Processing;
using Ezzygate.Integrations.Core.Scheduling;
using Ezzygate.Integrations.Mock;
using Ezzygate.Integrations.Ph3a;
using Ezzygate.Integrations.Ph3a.Api;
using Ezzygate.Integrations.Rapyd;

namespace Ezzygate.Integrations.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationProvider, IntegrationProvider>();
        services.AddScoped<ICreditCardIntegrationProcessor, CreditCardIntegrationProcessor>();
        services.AddScoped<IIntegrationProcessor, IntegrationProcessor>();
        services.AddScoped<IIntegrationFinalizer, IntegrationFinalizer>();

        services.AddScoped<ICreditCardIntegration, MockCreditCardIntegration>();
        services.AddScoped<ICreditCardIntegration, RapydIntegration>();

        services.AddDelayedTaskScheduler();

        services.AddScoped<CallbackFinalizeTask>();

        services.AddScoped<IPh3AService, Ph3AService>();
        services.AddScoped<Ph3AApiClient>();

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