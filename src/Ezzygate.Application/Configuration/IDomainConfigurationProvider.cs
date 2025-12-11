namespace Ezzygate.Application.Configuration;

public interface IDomainConfigurationProvider
{
    DomainConfiguration GetCurrentDomainConfiguration();
    DomainConfiguration GetDomainConfiguration(string host);
    IReadOnlyList<DomainConfiguration> GetAllDomainConfigurations();
}