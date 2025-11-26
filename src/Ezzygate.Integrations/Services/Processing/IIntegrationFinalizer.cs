using Ezzygate.Application.Integrations;

namespace Ezzygate.Integrations.Services.Processing;

public interface IIntegrationFinalizer
{
    Task<IntegrationResult> FinalizeAsync(FinalizeRequest request, CancellationToken cancellationToken = default);
}