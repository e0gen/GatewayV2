using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Core;

public class IntegrationProvider : IIntegrationProvider
{
    private readonly IServiceProvider _serviceProvider;

    public IntegrationProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public ICreditCardIntegration? GetCreditCardIntegration(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        var services = _serviceProvider.GetServices<ICreditCardIntegration>();
        return services.FirstOrDefault(service => string.Equals(service.Tag, tag, StringComparison.OrdinalIgnoreCase));
    }

    public T? GetIntegration<T>(string? tag) where T : class, IIntegration
    {
        if (string.IsNullOrWhiteSpace(tag))
            return null;

        var services = _serviceProvider.GetServices<T>();
        return services.FirstOrDefault(service => string.Equals(service.Tag, tag, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<T> GetAllIntegrations<T>() where T : class, IIntegration
    {
        return _serviceProvider.GetServices<T>();
    }
}