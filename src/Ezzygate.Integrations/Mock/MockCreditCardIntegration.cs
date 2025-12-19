using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Mock;

public class MockCreditCardIntegration : BaseIntegration, ICreditCardIntegration
{
    private static readonly string[] TestCards =
    [
        "4580000000000000", "4387751111111111", "5442987111111111", "4111111111111111", "371000911111111",
        "36005131111111", "3535111111111111", "5326300000000000", "5326140000000000", "4511742700707855",
        "4056850111111111", "5130113111111111", "6221701111111111", "91000000", "6299711111111111"
    ];

    private static bool IsTestCard(string carNumber) => TestCards.Contains(carNumber.Trim());

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

    public override Task<IntegrationResult> ProcessTransactionAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        return ctx.OpType == OperationType.Finalize ? FinalizeTrxAsync(ctx, cancellationToken) : ProcessTrxAsync(ctx, cancellationToken);
    }

    private async Task<IntegrationResult> ProcessTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        _logger.Info(LogTag.WebApi, "Processing mock credit card transaction. OpType: {OpType}, Amount: {Amount}, DebitRefCode: {DebitRefCode}",
            ctx.OpType, ctx.Amount, ctx.DebitRefCode);

        var integrationResult = new IntegrationResult
        {
            Code = "595"
        };

        var timeout = 0;
        if (IsTestCard(ctx.CardNumber))
        {
            integrationResult.Code = "000";
            switch (ctx.Amount)
            {
                case 0.04M:
                    integrationResult.Code = "1001";
                    break;
                case 0.05M:
                    integrationResult.Code = "1002";
                    break;
                case 0.90M:
                    timeout = 5;
                    break;
                case 0.91M:
                    timeout = 10;
                    break;
                case 0.92M:
                    timeout = 20;
                    break;
                case 0.93M:
                    timeout = 30;
                    break;
                case 0.94M:
                    timeout = 40;
                    break;
                case 0.95M:
                    timeout = 50;
                    break;
                case 0.96M:
                    timeout = 60;
                    break;
                case 0.97M:
                    timeout = 70;
                    break;
                case 0.98M:
                    timeout = 80;
                    break;
                case 0.99M:
                    timeout = 90;
                    break;
                default:
                    if (ctx.Amount < 1)
                        integrationResult.Code = "596";
                    break;
            }
        }

        if (timeout > 0)
            await Task.Delay(TimeSpan.FromSeconds(timeout), cancellationToken);

        if (ctx.OpType == OperationType.Sale || ctx.OpType == OperationType.Sale3DS ||
            ctx.OpType == OperationType.Authorization || ctx.OpType == OperationType.Authorization3DS ||
            ctx.OpType == OperationType.RecurringInit)
        {
            if (ctx.Amount is 553M or 55.3M or 1553M)
            {
                integrationResult.Code = "553";
                var approveUrl = ctx.GetFinalizeUrl(FinalizeUrlType.SuccessRedirect);
                var declineUrl = ctx.GetFinalizeUrl(FinalizeUrlType.FailureRedirect);
                integrationResult.RedirectUrl = ctx.Get3dsAuthUrl(approveUrl, declineUrl);
            }
        }

        integrationResult.ApprovalNumber = "cn-" + Guid.NewGuid();

        Logger.Info(LogTag.WebApi, "Mock credit card transaction completed. Code: {Code}, Message: {Message}",
            integrationResult.Code, integrationResult.Message);

        return integrationResult;
    }

    private async Task<IntegrationResult> FinalizeTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        if (!ctx.CheckFinalizeUrl())
            return new IntegrationResult { Code = "500", Message = "Return URL signature mismatch" };

        var integrationResult = ctx.GetIntegrationResult();

        var type = Enum.Parse<FinalizeUrlType>(ctx.QueryStringParsed["type"] ?? string.Empty);
        integrationResult.Code = type switch
        {
            FinalizeUrlType.FailureRedirect => "599",
            FinalizeUrlType.SuccessRedirect => "000",
            _ => integrationResult.Code
        };

        var moveResult = await CompleteFinalizationAsync(ctx, integrationResult, cancellationToken);

        integrationResult.RecurringId = await CreateRecurringAsync(integrationResult.Code.NotNull(),
            ctx, moveResult.TrxId, moveResult.PendingTrx.TransType, ctx.ChargeAttemptLogId, cancellationToken);

        return integrationResult;
    }

    public override Task<string> GetNotificationResponseAsync(TransactionContext ctx,
        CancellationToken cancellationToken = default)
    {
        Logger.Debug("Returning mock notification response for DebitRefCode: {DebitRefCode}", ctx.DebitRefCode);
        return base.GetNotificationResponseAsync(ctx, cancellationToken);
    }

    public override Task MaintainAsync(CancellationToken cancellationToken = default)
    {
        Logger.Debug("Performing mock maintenance for {Tag} integration", Tag);
        return base.MaintainAsync(cancellationToken);
    }
}