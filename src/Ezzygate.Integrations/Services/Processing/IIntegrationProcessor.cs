using Ezzygate.Application.Integrations;

namespace Ezzygate.Integrations.Services.Processing;

public interface IIntegrationProcessor
{
    Task<IntegrationResult> ProcessAsync(ProcessRequest request, CancellationToken cancellationToken = default);
}