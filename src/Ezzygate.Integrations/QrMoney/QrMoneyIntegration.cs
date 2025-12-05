using Microsoft.Extensions.Logging;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Notifications;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core;
using Ezzygate.Integrations.Core.Abstractions;
using Ezzygate.Integrations.Extensions;
using Ezzygate.Integrations.QrMoney.Api;

namespace Ezzygate.Integrations.QrMoney;

public class QrMoneyIntegration : BaseIntegration, ICreditCardIntegration
{
    private const int DebitCompanyId = 111;
    private readonly IQrMoneyApiClient _apiClient;
    private readonly ILogger<QrMoneyIntegration> _logger;

    public QrMoneyIntegration(
        ILogger<QrMoneyIntegration> logger,
        IIntegrationDataService dataService,
        INotificationClient notificationClient,
        IQrMoneyApiClient qrMoneyApiClient)
        : base(logger, dataService, notificationClient)
    {
        _apiClient = qrMoneyApiClient;
        _logger = logger;
    }

    public override string Tag => "QrMoney";

    public override Task<IntegrationResult> ProcessTransactionAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        switch (ctx.OpType)
        {
            case OperationType.Finalize:
                return FinalizeTrxAsync(ctx, cancellationToken);
            case OperationType.Refund:
                return RefundTrxAsync(ctx, cancellationToken);
            case OperationType.Sale:
            case OperationType.Sale3DS:
            case OperationType.RecurringInit:
            case OperationType.RecurringInit3DS:
            case OperationType.RecurringSale:
                return ProcessTrxAsync(ctx, cancellationToken);
            default:
                throw new Exception("Operation not supported");
        }
    }

    private async Task<IntegrationResult> RefundTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(RefundTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var refundResult = await _apiClient.RefundRequestAsync(ctx, cancellationToken);

        logger.Info($"[RefundRequest] Code: {refundResult.StatusCode} Path: {refundResult.Path}");
        logger.Info($"[RefundRequest] Req: {refundResult.RequestJson}");
        logger.Info($"[RefundRequest] Res: {refundResult.ResponseJson}");

        if (refundResult.Response is null)
            throw new Exception("Failed to parse refund response", refundResult.Exception);

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = ctx.ApprovalNumber;
        integrationResult.Code = refundResult.StatusCode == 200 ? "000" : "99";
        integrationResult.Message = refundResult.StatusCode == 200 ? "Refund Successful" : "Refund Failed";

        logger.SetShortMessage($"Status: {refundResult.StatusCode} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> ProcessTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(ProcessTrxAsync));
        try
        {
            ValidateDebitCompanyId(ctx, DebitCompanyId);

            logger.Info($"BrowserAcceptContent: {ctx.FormDataParsed.GetValueOrDefault("browserAcceptContent")}");
            logger.Info($"BrowserColorDepth: {ctx.FormDataParsed.GetValueOrDefault("browserColorDepth")}");
            logger.Info($"ClientIp: {ctx.ClientIp}");
            logger.Info($"BrowserJavaEnabled: {ctx.FormDataParsed.GetValueOrDefault("browserJavaEnabled")}");
            logger.Info($"BrowserJavaScriptEnabled: {ctx.FormDataParsed.GetValueOrDefault("browserJavaScriptEnabled")}");
            logger.Info($"BrowserLanguage: {ctx.FormDataParsed.GetValueOrDefault("browserLanguage")}");
            logger.Info($"BrowserScreenHeight: {ctx.FormDataParsed.GetValueOrDefault("browserScreenHeight")}");
            logger.Info($"BrowserScreenWidth: {ctx.FormDataParsed.GetValueOrDefault("browserScreenWidth")}");
            logger.Info($"BrowserTimezoneOffset: {ctx.FormDataParsed.GetValueOrDefault("browserTimezoneOffset")}");
            logger.Info($"BrowserUserAgent: {ctx.FormDataParsed.GetValueOrDefault("browserUserAgent")}");

            var processResult = await _apiClient.PaymentRequestAsync(ctx, cancellationToken);

            logger.Info($"[PaymentRequest] Code: {processResult.StatusCode} Path: {processResult.Path}");
            logger.Info($"[PaymentRequest] Req: {processResult.RequestJson}");
            logger.Info($"[PaymentRequest] Res: {processResult.ResponseJson}");

            await DataService.ChargeAttempts.UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                .SetInnerRequest(processResult.RequestJson)
                .SetInnerResponse(processResult.ResponseJson), cancellationToken);

            var integrationResult = ctx.GetIntegrationResult();

            if (processResult.StatusCode is < 200 or > 299)
            {
                integrationResult.Code = "99";
                if (processResult.ResponseJson.StartsWith('\"'))
                {
                    var errorMessage = processResult.ResponseJson.Substring(1, processResult.ResponseJson.Length - 2);
                    integrationResult.Message = $"Payment Failed, {errorMessage}";
                }
                else
                {
                    var error = processResult.ResponseJson.Deserialize<QrMoneyError>();
                    integrationResult.Message = $"Payment Failed, {error?.Title} {error?.Errors}";
                }
            }
            else
            {
                var response = processResult.Response;
                if (response is null)
                    throw new Exception("Failed to parse process response", processResult.Exception);

                integrationResult.ApprovalNumber = response.Data;

                if (response.ChallengeRequired)
                {
                    if (response.RedirectUrl == null)
                        throw new Exception("Redirect link not found");

                    var collectUrl = ctx.GetCollectUrl(response.RedirectUrl);
                    integrationResult.Code = "553";
                    integrationResult.Message = "Payment Pending. Challenge required";
                    integrationResult.RedirectUrl = collectUrl;
                }
                else
                {
                    integrationResult.Code = "000";
                    integrationResult.Message = "Payment Processed";
                }
            }

            logger.SetShortMessage($"Status: {processResult.StatusCode} Code: {integrationResult.Code}");
            return integrationResult;
        }
        catch (Exception e)
        {
            logger.Error($"Error in QrMoney process: {e.Message}\n\n{e.StackTrace}");
            return new IntegrationResult { Code = "500", Message = "Internal server error: process" };
        }
    }

    private async Task<IntegrationResult> FinalizeTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(FinalizeTrxAsync));
        logger.Info($"Source: {(ctx.IsAutomatedRequest ? "Webhook" : "Redirect")}");
        try
        {
            if (!ctx.CheckFinalizeUrl() && !ctx.IsAutomatedRequest)
                return new IntegrationResult { Code = "500", Message = "Return URL signature mismatch" };

            ctx.TransType = ctx.CreditType;
            ctx.DebitRefNum = ctx.Terminal?.TerminalNumber;

            var pendingTrx = await DataService.Transactions.GetPendingTrxByIdAsync(ctx.LocatedTrx!.TrxId, cancellationToken);
            if (pendingTrx == null)
                throw new Exception($"Finalize pending trx '{ctx.LocatedTrx.TrxId}' not found");
            var log = await DataService.ChargeAttempts.GetByIdAsync(ctx.ChargeAttemptLogId);
            if (log is null)
                throw new Exception($"Charge attempt log not found for id '{ctx.ChargeAttemptLogId}'");

            var paymentResult = await _apiClient.StatusRequestAsync(ctx, cancellationToken);

            logger.Info($"[GetPaymentStatus] Code: {paymentResult.StatusCode} Path: {paymentResult.Path}");
            logger.Info($"[GetPaymentStatus] Res: {paymentResult.ResponseJson}");

            var paymentResponse = paymentResult.Response;
            if (paymentResponse is null)
                throw new Exception("Failed to parse finalize response", paymentResult.Exception);

            var requestStatus = paymentResponse.PaymentRequestStatusId switch
            {
                0 => "waiting",
                1 => "paid",
                2 => "unpaid",
                3 => "cancelled",
                4 => "pending",
                5 => "archived",
                _ => "unknown"
            };

            var status = paymentResponse.TransactionStatusId switch
            {
                0 => "waiting",
                1 => "approved",
                2 => "declined",
                3 => "pending",
                _ => "unknown"
            };

            var isSuccess = paymentResponse.TransactionStatusId == 1;
            var isPending = paymentResponse.TransactionStatusId is 0 or 3;
            var errorMessage = paymentResponse.Notes;

            logger.Info($"Status: {status}, Request Status: {requestStatus}");
            if (!string.IsNullOrEmpty(errorMessage)) logger.Info($"ErrorMessage: {errorMessage}");

            await DataService.ChargeAttempts
                .UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                    .SetInnerRequest($"Callback: {ctx.IsAutomatedRequest}\n{log.InnerRequest}\n{ctx.GetFinalizeUrl(ctx.FinalizeType)}")
                    .SetInnerResponse($"{paymentResult.ResponseJson}\n{log.InnerResponse}"), cancellationToken);

            logger.Info($"Status: {status} TransType: {ctx.LocatedTrx.TransType} Approval number: {ctx.ApprovalNumber}");

            var integrationResult = ctx.GetIntegrationResult();

            if (ctx.IsAutomatedRequest)
            {
                logger.Info("Automated request");
                if (isSuccess)
                {
                    integrationResult.Code = "000";
                    integrationResult.Message = "Payment Processed";
                }
                else if (isPending)
                {
                    integrationResult.Code = "001";
                    integrationResult.Message = "Payment Pending";
                }
                else
                {
                    integrationResult.Code = "99";
                    integrationResult.Message = "Payment Failed, " + errorMessage;
                }
            }
            else
            {
                logger.Info($"Redirection request. Type: {ctx.FinalizeType}");
                switch (ctx.FinalizeType)
                {
                    case FinalizeUrlType.SuccessRedirect:
                        integrationResult.Code = "000";
                        integrationResult.Message = "Payment Processed";
                        break;
                    case FinalizeUrlType.FailureRedirect:
                        integrationResult.Code = "99";
                        integrationResult.Message = "Payment Failed, " + errorMessage;
                        break;
                }
            }

            await CompleteFinalizationAsync(ctx, integrationResult, cancellationToken);

            logger.SetShortMessage($"Status: {status} Approval Number: {integrationResult.ApprovalNumber} Pass/Fail Id: {integrationResult.TrxId} Code: {integrationResult.Code}");

            return integrationResult;
        }
        catch (Exception e)
        {
            logger.Error($"Error in QrMoney finalization: {e.Message}\n\n{e.StackTrace}");
            return new IntegrationResult { Code = "500", Message = "Internal server error: finalization" };
        }
    }
}