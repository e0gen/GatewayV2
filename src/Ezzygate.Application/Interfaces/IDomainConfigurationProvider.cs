using Ezzygate.Application.Configuration;

namespace Ezzygate.Application.Interfaces;

public interface IDomainConfigurationProvider
{
    IDomainConfiguration GetCurrentDomainConfiguration();
    IDomainConfiguration GetDomainConfiguration(string host);
    IReadOnlyList<IDomainConfiguration> GetAllDomainConfigurations();
}