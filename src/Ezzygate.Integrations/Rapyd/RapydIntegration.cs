using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Services;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Scheduling;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core;
using Ezzygate.Integrations.Core.Abstractions;
using Ezzygate.Integrations.Core.Scheduling;
using Ezzygate.Integrations.Rapyd.Api;

namespace Ezzygate.Integrations.Rapyd;

public class RapydIntegration : BaseIntegration, ICreditCardIntegration
{
    private const int DebitCompanyId = 109;
    private readonly IRapydApiClient _apiClient;
    private readonly ILogger<RapydIntegration> _logger;
    private readonly ICreditCardService _creditCardService;
    private readonly IDelayedTaskScheduler _taskScheduler;

    public RapydIntegration(
        ILogger<RapydIntegration> logger,
        IIntegrationDataService dataService,
        INotificationClient notificationClient,
        IRapydApiClient rapydApiClient,
        ICreditCardService creditCardService,
        IDelayedTaskScheduler taskScheduler)
        : base(logger, dataService, notificationClient)
    {
        _apiClient = rapydApiClient;
        _logger = logger;
        _creditCardService = creditCardService;
        _taskScheduler = taskScheduler;
    }

    public override string Tag => "Rapyd";

    public override Task<IntegrationResult> ProcessTransactionAsync(TransactionContext ctx,
        CancellationToken cancellationToken = default)
    {
        switch (ctx.OpType)
        {
            case OperationType.Finalize:
                return FinalizeTrxAsync(ctx, cancellationToken);
            case OperationType.Refund:
                return RefundTrxAsync(ctx);
            case OperationType.AuthorizationCapture:
                return CaptureTrxAsync(ctx);
            case OperationType.Authorization:
            case OperationType.Authorization3DS:
                return ProcessTrxAsync(ctx, false);
            case OperationType.Sale:
            case OperationType.Sale3DS:
            case OperationType.RecurringInit:
            case OperationType.RecurringInit3DS:
            case OperationType.RecurringSale:
                return ProcessTrxAsync(ctx);
            default:
                throw new Exception("Operation not supported");
        }
    }

    private async Task<IntegrationResult> CaptureTrxAsync(TransactionContext ctx)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(CaptureTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var captureResult = await _apiClient.CaptureRequest(ctx);

        logger.Info($"[CaptureRequest] Code: {captureResult.StatusCode} Path: {captureResult.Path}");
        logger.Info($"[CaptureRequest] Req: {captureResult.RequestJson}");
        logger.Info($"[CaptureRequest] Res: {captureResult.ResponseJson}");

        if (captureResult.Response is null)
            throw new Exception("Failed to parse capture response", captureResult.Exception);
        var response = captureResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = response.Data.Id;
        integrationResult.Code = response.Status.Status == "SUCCESS" ? "000" : response.Status.ErrorCode;
        integrationResult.Message = response.Status.Status == "SUCCESS" ? "Amount Captured" : response.Status.Message;

        logger.SetShortMessage($"Status: {response.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> RefundTrxAsync(TransactionContext ctx)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(RefundTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var refundResult = await _apiClient.RefundRequest(ctx);

        logger.Info($"[RefundRequest] Code: {refundResult.StatusCode} Path: {refundResult.Path}");
        logger.Info($"[RefundRequest] Req: {refundResult.RequestJson}");
        logger.Info($"[RefundRequest] Res: {refundResult.ResponseJson}");

        if (refundResult.Response is null)
            throw new Exception("Failed to parse refund response", refundResult.Exception);
        var response = refundResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = response.Data.Id;
        integrationResult.Code = response.Status.Status == "SUCCESS" ? "000" : "99";
        integrationResult.Message = response.Status.Status == "SUCCESS" ? "Refund Successful" : response.Status.Message;

        logger.SetShortMessage($"Status: {response.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> ProcessTrxAsync(TransactionContext ctx, bool capture = true)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(ProcessTrxAsync));

        var midCountry = !string.IsNullOrEmpty(ctx.Terminal.AccountSubId) ? ctx.Terminal.AccountSubId : "xx";

        var g = Guid.NewGuid();
        var guid = g.GetHashCode();

        if (guid < 0)
            guid *= -1;

        var cc = await _creditCardService.FromCardNumberAsync(ctx.CardNumber);
        if (cc.PaymentMethodId != PaymentMethodEnum.CCMastercard &&
            cc.PaymentMethodId != PaymentMethodEnum.CCVisa)
            return new IntegrationResult { Code = "99", Message = "Invalid Card" };

        ctx.TransType = ctx.CreditType;
        ctx.TrxId = guid;
        ctx.OrderId = guid.ToString();
        ctx.ApprovalNumber = guid.ToString();
        ctx.DebitRefNum = ctx.Terminal.TerminalNumber;

        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var processResult = await _apiClient.ProcessRequest(ctx, midCountry, capture, cc.PaymentMethodId);

        logger.Info($"[ProcessRequest] Code: {processResult.StatusCode} Path: {processResult.Path}");
        logger.Info($"[ProcessRequest] Req: {processResult.RequestJson}");
        logger.Info($"[ProcessRequest] Res: {processResult.ResponseJson}");

        await DataService.ChargeAttempts.UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
            .SetInnerRequest(processResult.RequestJson)
            .SetInnerResponse(processResult.ResponseJson));

        if (processResult.Response is null)
            throw new Exception("Failed to parse process response", processResult.Exception);
        var response = processResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = response.Data.Id;

        if (response.Status.Status != "SUCCESS")
        {
            integrationResult.Code = "99";
            integrationResult.Message = response.Status.ErrorCode + ": " + response.Status.Message;
            return integrationResult;
        }

        if (response.Data is { Status: "ACT", NextAction: "3d_verification" } &&
            response.Data.RedirectUrl != "")
        {
            var collectUrl = ctx.GetCollectUrl(response.Data.RedirectUrl.NotNull());
            integrationResult.Code = "553";
            integrationResult.Message = "Payment Pending. Challenge required";
            integrationResult.RedirectUrl = collectUrl;

            logger.Info("Status: Pending. Scheduling a timeout job");
            ScheduleTimeoutJob(ctx.DebitRefCode, ctx.ChargeAttemptLogId, TimeSpan.FromMinutes(15));
        }
        else
        {
            integrationResult.Code = "000";
            integrationResult.Message = response.Status.Message;
        }

        logger.SetShortMessage($"Status: {response.Data.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> FinalizeTrxAsync(TransactionContext ctx,
        CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(FinalizeTrxAsync));
        logger.Info($"Source: {(ctx.IsAutomatedRequest ? "Webhook" : "Redirect")}");
        try
        {
            if (!ctx.CheckFinalizeUrl() && !ctx.IsAutomatedRequest)
                return new IntegrationResult { Code = "500", Message = "Return URL signature mismatch" };

            ctx.TransType = ctx.CreditType;
            ctx.DebitRefNum = ctx.Terminal.TerminalNumber;

            var pendingTrx = await DataService.Transactions.GetPendingTrxByIdAsync(ctx.LocatedTrx.TrxId, cancellationToken);
            if (pendingTrx == null)
                throw new Exception($"Finalize pending trx '{ctx.LocatedTrx.TrxId}' not found");
            var log = await DataService.ChargeAttempts.GetByIdAsync(ctx.ChargeAttemptLogId);
            if (log is null)
                throw new Exception($"Charge attempt log not found for id '{ctx.ChargeAttemptLogId}'");

            if (ctx.AutomatedStatus == TimeoutFinalizeTask.EventName)
            {
                logger.Info("AutoFinalize");
                return await AutoFinalizeTrxAsync(ctx, cancellationToken);
            }

            var paymentResult = await _apiClient.StatusRequest(ctx);

            logger.Info($"[GetPayment] Code: {paymentResult.StatusCode} Path: {paymentResult.Path}");
            logger.Info($"[GetPayment] Res: {paymentResult.ResponseJson}");

            if (paymentResult.Response is null)
                throw new Exception("Failed to parse finalize response", paymentResult.Exception);
            var paymentResponse = paymentResult.Response;

            var isSuccess = paymentResponse.Status.Status == "SUCCESS";
            var status = isSuccess ? paymentResponse.Data.Status : paymentResponse.Status.Status;
            var errorMessage = isSuccess ? paymentResponse.Data.FailureMessage : paymentResponse.Status.Message;

            if (string.IsNullOrEmpty(errorMessage)) logger.Info($"ErrorMessage: {errorMessage}");

            await DataService.ChargeAttempts
                .UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                    .SetInnerRequest($"Callback: {ctx.IsAutomatedRequest}\n{log.InnerRequest}\n{ctx.GetFinalizeUrl(ctx.FinalizeType)}")
                    .SetInnerResponse($"{paymentResult.ResponseJson}\n{log.InnerRequest}"), cancellationToken);

            logger.Info($"Status: {status} TransType: {ctx.LocatedTrx.TransType} Approval number: {ctx.ApprovalNumber}");
            logger.Info(ctx.IsAutomatedRequest
                ? "Automated request"
                : $"Redirection request. Type: {ctx.FinalizeType}");

            var integrationResult = ctx.GetIntegrationResult();

            switch (status)
            {
                case "CLO":
                    integrationResult.Code = "000";
                    integrationResult.Message = "Payment Processed";
                    break;
                default:
                    integrationResult.Code = "99";
                    integrationResult.Message = "Payment Failed, " + errorMessage;
                    break;
            }

            await CompleteFinalizationAsync(ctx, integrationResult, cancellationToken);

            logger.SetShortMessage(
                $"Status: {status} Approval Number: {integrationResult.ApprovalNumber} Pass/Fail Id: {integrationResult.TrxId} Code: {integrationResult.Code}");

            return integrationResult;
        }
        catch (Exception e)
        {
            logger.Error($"Error in Rapyd finalization: {e.Message}\n\n{e.StackTrace}");
            return new IntegrationResult { Code = "500", Message = "Internal server error: finalization" };
        }
    }

    private void ScheduleTimeoutJob(string debitReferenceCode, int chargeAttemptLogId, TimeSpan timeout)
    {
        var payload = new TimeoutFinalizePayload
        {
            Tag = Tag,
            DebitReferenceCode = debitReferenceCode,
            ChargeAttemptLogId = chargeAttemptLogId
        };

        _taskScheduler.Schedule<TimeoutFinalizeTask, TimeoutFinalizePayload>(payload, timeout);
    }
}