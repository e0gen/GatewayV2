using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Mock;

public class MockCreditCardIntegration : BaseIntegration, ICreditCardIntegration
{
    private readonly ILogger<MockCreditCardIntegration> _logger;

    public MockCreditCardIntegration(
        ILogger<MockCreditCardIntegration> logger,
        IIntegrationDataService dataService,
        INotificationClient notificationClient)
        : base(logger, dataService, notificationClient)
    {
        _logger = logger;
    }

    public override string Tag => "Mock";

    public override async Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        _logger.Info(LogTag.WebApi,
            "Processing mock credit card transaction. OpType: {OpType}, Amount: {Amount}, DebitRefCode: {DebitRefCode}",
            context.OpType, context.Amount, context.DebitRefCode);

        await Task.Delay(100, cancellationToken);

        var isApproved = context.Amount < 100;

        var result = new IntegrationResult
        {
            Code = isApproved ? "000" : "001",
            Message = isApproved ? "Transaction approved" : "Transaction declined",
            DebitRefCode = context.DebitRefCode,
            DebitRefNum = context.DebitRefNum,
            ApprovalNumber = isApproved ? $"MOCK{DateTime.UtcNow:yyyyMMddHHmmss}" : null,
            TrxId = Random.Shared.Next(100000, 999999),
            IsFinalized = true
        };

        Logger.Info(LogTag.WebApi, "Mock credit card transaction completed. Code: {Code}, Message: {Message}",
            result.Code, result.Message);

        return result;
    }

    public override Task<string> GetNotificationResponseAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        Logger.Debug("Returning mock notification response for DebitRefCode: {DebitRefCode}", context.DebitRefCode);
        return Task.FromResult("mock_notification_ok");
    }

    public override Task MaintainAsync(CancellationToken cancellationToken = default)
    {
        Logger.Debug("Performing mock maintenance for {Tag} integration", Tag);
        return Task.CompletedTask;
    }
}