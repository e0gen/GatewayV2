using Ezzygate.Application.Integrations;

namespace Ezzygate.Integrations.Core.Processing;

public interface IIntegrationFinalizer
{
    Task<IntegrationResult> FinalizeAsync(FinalizeRequest request, CancellationToken cancellationToken = default);
}