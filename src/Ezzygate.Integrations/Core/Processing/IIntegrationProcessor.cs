using Ezzygate.Application.Integrations;

namespace Ezzygate.Integrations.Core.Processing;

public interface IIntegrationProcessor
{
    Task<IntegrationResult> ProcessAsync(ProcessRequest request, CancellationToken cancellationToken = default);
}