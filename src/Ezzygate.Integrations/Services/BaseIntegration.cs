using System;
using Microsoft.Extensions.Logging;
using Ezzygate.Integrations.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Transactions;

namespace Ezzygate.Integrations.Services;

public abstract class BaseIntegration : IIntegration
{
    protected readonly ILogger<BaseIntegration> Logger;

    protected BaseIntegration(ILogger<BaseIntegration> logger)
    {
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public abstract string Tag { get; }

    public abstract Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
        CancellationToken cancellationToken = default);

    public virtual Task<string> GetNotificationResponseAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult("ok");
    }

    public virtual Task MaintainAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}