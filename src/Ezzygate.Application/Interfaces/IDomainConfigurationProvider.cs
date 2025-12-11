using Ezzygate.Application.Configuration;

namespace Ezzygate.Application.Interfaces;

public interface IDomainConfigurationProvider
{
    DomainConfiguration GetCurrentDomainConfiguration();
    DomainConfiguration GetDomainConfiguration(string host);
    IReadOnlyList<DomainConfiguration> GetAllDomainConfigurations();
}