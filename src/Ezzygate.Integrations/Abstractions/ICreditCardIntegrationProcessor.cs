using System.Threading;
using System.Threading.Tasks;
using Ezzygate.Application.Models;

namespace Ezzygate.Integrations.Abstractions;

public interface ICreditCardIntegrationProcessor
{
    Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context, CancellationToken cancellationToken = default);
}