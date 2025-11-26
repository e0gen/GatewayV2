using Microsoft.AspNetCore.Http;

namespace Ezzygate.Integrations.Core.Events;

public interface IIntegrationEventHandler
{
    string Tag { get; }
    Task HandleAsync(HttpRequest request, CancellationToken cancellationToken = default);
}