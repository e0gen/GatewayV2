using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Integrations.Abstractions;

public interface ICreditCardIntegrationProcessor
{
    Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context, CancellationToken cancellationToken = default);
}