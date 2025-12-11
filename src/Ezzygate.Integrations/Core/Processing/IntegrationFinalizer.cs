using Microsoft.Extensions.Logging;
using Ezzygate.Application.Configuration;
using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Locking;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Core.Processing;

public sealed class IntegrationFinalizer : IIntegrationFinalizer
{
    private readonly ILogger<IntegrationFinalizer> _logger;
    private readonly ITransactionContextFactory _transactionContextFactory;
    private readonly ICreditCardIntegrationProcessor _integrationProcessor;
    private readonly IDistributedLockService _distributedLockService;
    private readonly DomainConfiguration _domainConfiguration;

    public IntegrationFinalizer(
        ILogger<IntegrationFinalizer> logger,
        ITransactionContextFactory transactionContextFactory,
        ICreditCardIntegrationProcessor integrationProcessor,
        IDistributedLockService distributedLockService,
        DomainConfiguration domainConfiguration)
    {
        _logger = logger;
        _transactionContextFactory = transactionContextFactory;
        _integrationProcessor = integrationProcessor;
        _distributedLockService = distributedLockService;
        _domainConfiguration = domainConfiguration;
    }

    public async Task<IntegrationResult> FinalizeAsync(FinalizeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.DebitReferenceCode))
        {
            return new IntegrationResult
            {
                Code = "520",
                Message = "Invalid transaction reference id",
                DebitRefCode = request.DebitReferenceCode
            };
        }

        var lockKey = $"process_finalize_{request.DebitReferenceCode}";

        try
        {
            await using var distributedLock =
                await _distributedLockService.AcquireLockAsync(lockKey, cancellationToken: cancellationToken);

            _logger.Info(LogTag.Integration, "Finalize request for DebitRefCode: {DebitRefCode}",
                request.DebitReferenceCode);

            var ctx = await _transactionContextFactory.CreateAsync(request.DebitReferenceCode,
                request.ChargeAttemptLogId);

            ctx.OpType = request.OperationType;
            ctx.DebitRefCode = request.DebitReferenceCode;
            ctx.DebitRefNum = request.DebitReferenceNum ?? string.Empty;
            ctx.RequestContent = request.RequestContent;
            ctx.FormData = request.FormData;
            ctx.QueryString = request.QueryString;
            ctx.IsAutomatedRequest = request.IsAutomatedRequest;
            ctx.AutomatedStatus = request.AutomatedStatus;
            ctx.AutomatedCode = request.AutomatedCode;
            ctx.AutomatedMessage = request.AutomatedMessage;
            ctx.AutomatedPayload = request.AutomatedPayload;

            var result = await _integrationProcessor.ProcessTransactionAsync(ctx, cancellationToken);

            if (_domainConfiguration.DisablePostRedirectUrl)
                result.RedirectUrl = null;

            return result;
        }
        catch (DistributedLockException ex)
        {
            _logger.Error(LogTag.Integration, ex, "Failed to acquire lock for Finalize: DebitRefCode: {DebitRefCode}",
                request.DebitReferenceCode);

            return new IntegrationResult
            {
                Code = "520",
                Message = "[003] Transaction is being processed, please retry",
                DebitRefCode = request.DebitReferenceCode
            };
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.Integration, ex, "Finalize exception for DebitRefCode: {DebitRefCode}",
                request.DebitReferenceCode);

            return new IntegrationResult
            {
                Code = "520",
                Message = "[003] Internal server error during finalization",
                DebitRefCode = request.DebitReferenceCode
            };
        }
    }
}