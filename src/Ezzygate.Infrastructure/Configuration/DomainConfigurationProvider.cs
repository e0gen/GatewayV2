using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Ezzygate.Application.Configuration;
using Ezzygate.Application.Interfaces;

namespace Ezzygate.Infrastructure.Configuration;

public class DomainConfigurationProvider : IDomainConfigurationProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationConfiguration _applicationConfiguration;
    private readonly DomainConfiguration _defaultDomain;

    public DomainConfigurationProvider(
        IHttpContextAccessor httpContextAccessor,
        IOptions<ApplicationConfiguration> applicationConfiguration)
    {
        _httpContextAccessor = httpContextAccessor;
        _applicationConfiguration = applicationConfiguration.Value;
        _defaultDomain = _applicationConfiguration.Domains
                             .FirstOrDefault(d => string.Equals(d.Host, "default", StringComparison.OrdinalIgnoreCase)) 
                         ?? _applicationConfiguration.Domains.FirstOrDefault()
                         ?? new DomainConfiguration { Host = "default" };
    }

    public DomainConfiguration GetCurrentDomainConfiguration()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return _defaultDomain;

        var host = httpContext.Request.Host.Host;
        return GetDomainConfiguration(host);
    }

    public DomainConfiguration GetDomainConfiguration(string host)
    {
        if (string.IsNullOrEmpty(host))
            return _defaultDomain;

        var domain = _applicationConfiguration.Domains
            .FirstOrDefault(d => string.Equals(d.Host, host, StringComparison.OrdinalIgnoreCase));

        if (domain != null)
            return domain;

        domain = _applicationConfiguration.Domains
            .FirstOrDefault(d =>
                !string.Equals(d.Host, "default", StringComparison.OrdinalIgnoreCase) &&
                (host.Contains(d.Host, StringComparison.OrdinalIgnoreCase) ||
                 d.Host.Contains(host, StringComparison.OrdinalIgnoreCase)));

        return domain ?? _defaultDomain;
    }

    public IReadOnlyList<DomainConfiguration> GetAllDomainConfigurations()
    {
        return _applicationConfiguration.Domains.AsReadOnly();
    }
}