using System.Threading;
using System.Threading.Tasks;
using Ezzygate.Infrastructure.Services;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.Integrations.Abstractions;

public interface ICreditCardIntegrationProcessor
{
    Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context, CancellationToken cancellationToken = default);
}