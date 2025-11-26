namespace Ezzygate.Integrations.Core.Abstractions;

public interface IIntegrationProvider
{
    ICreditCardIntegration? GetCreditCardIntegration(string? tag);
    T? GetIntegration<T>(string? tag) where T : class, IIntegration;
    IEnumerable<T> GetAllIntegrations<T>() where T : class, IIntegration;
}