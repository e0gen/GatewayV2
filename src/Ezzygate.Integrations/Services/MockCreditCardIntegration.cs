using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Ezzygate.Infrastructure.Services;
using Ezzygate.Integrations.Abstractions;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.Integrations.Services;

public class MockCreditCardIntegration : BaseIntegration, ICreditCardIntegration
{
    public MockCreditCardIntegration(ILogger<MockCreditCardIntegration> logger) : base(logger)
    {
    }

    public override string Tag => "Mock";

    public override async Task<IntegrationResult> ProcessTransactionAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        Logger.LogInformation(
            "Processing mock credit card transaction. OpType: {OpType}, Amount: {Amount}, DebitRefCode: {DebitRefCode}",
            context.OpType, context.Amount, context.DebitRefCode);

        // Simulate processing delay
        await Task.Delay(100, cancellationToken);

        // Mock logic: approve transactions under $100, decline above
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

        Logger.LogInformation("Mock credit card transaction completed. Code: {Code}, Message: {Message}",
            result.Code, result.Message);

        return result;
    }

    public override Task<string> GetNotificationResponseAsync(TransactionContext context,
        CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Returning mock notification response for DebitRefCode: {DebitRefCode}", context.DebitRefCode);
        return Task.FromResult("mock_notification_ok");
    }

    public override Task MaintainAsync(CancellationToken cancellationToken = default)
    {
        Logger.LogDebug("Performing mock maintenance for {Tag} integration", Tag);
        return Task.CompletedTask;
    }
}