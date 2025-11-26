using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Integrations.Core.Abstractions;

public interface IIntegration
{
    string Tag { get; }
    Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context, CancellationToken cancellationToken = default);
    Task<string> GetNotificationResponseAsync(TransactionContext context, CancellationToken cancellationToken = default);
    Task MaintainAsync(CancellationToken cancellationToken = default);
}