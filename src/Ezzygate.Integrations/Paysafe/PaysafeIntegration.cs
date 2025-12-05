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
using Ezzygate.Integrations.Paysafe.Api;

namespace Ezzygate.Integrations.Paysafe;

public class PaysafeIntegration : BaseIntegration, ICreditCardIntegration
{
    private const int DebitCompanyId = 110;
    private readonly IPaysafeApiClient _apiClient;
    private readonly ILogger<PaysafeIntegration> _logger;

    public PaysafeIntegration(
        ILogger<PaysafeIntegration> logger,
        IIntegrationDataService dataService,
        INotificationClient notificationClient,
        IPaysafeApiClient paysafeApiClient)
        : base(logger, dataService, notificationClient)
    {
        _apiClient = paysafeApiClient;
        _logger = logger;
    }

    public override string Tag => "Paysafe";

    public override Task<IntegrationResult> ProcessTransactionAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        switch (ctx.OpType)
        {
            case OperationType.Finalize:
                return FinalizeTrxAsync(ctx, cancellationToken);
            case OperationType.Refund:
                return RefundTrxAsync(ctx, cancellationToken);
            case OperationType.AuthorizationCapture:
                return CaptureTrxAsync(ctx, cancellationToken);
            case OperationType.AuthorizationRelease:
                return VoidTrxAsync(ctx, cancellationToken);
            case OperationType.Authorization:
            case OperationType.Authorization3DS:
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

    private async Task<IntegrationResult> ProcessTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(ProcessTrxAsync));

        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var handlerResult = await _apiClient.CreatePaymentHandlerAsync(ctx, cancellationToken);

        logger.Info($"[PaymentHandler] Code: {handlerResult.StatusCode} Path: {handlerResult.Path}");
        logger.Info($"[PaymentHandler] Req: {handlerResult.RequestJson}");
        logger.Info($"[PaymentHandler] Res: {handlerResult.ResponseJson}");

        await DataService.ChargeAttempts.UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
            .SetInnerRequest(handlerResult.RequestJson)
            .SetInnerResponse(handlerResult.ResponseJson), cancellationToken);

        if (handlerResult.Response is null)
            throw new Exception("Failed to parse payment handler response", handlerResult.Exception);
        var handlerResponse = handlerResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = handlerResponse.Id;

        if (handlerResponse.Error != null)
        {
            integrationResult.Code = "99";
            integrationResult.Message = handlerResponse.Error.ToMessage();
        }
        else
        {
            if (handlerResponse is { Status: "INITIATED", Action: "REDIRECT" })
            {
                var redirectUrl = handlerResponse.Links?.FirstOrDefault(x => x.Rel == "redirect_payment");
                if (redirectUrl == null)
                    throw new Exception("Redirect link not found");

                var collectUrl = ctx.GetCollectUrl(redirectUrl.Href.NotNull(), "direct");
                integrationResult.Code = "553";
                integrationResult.Message = "Payment Pending. Challenge required";
                integrationResult.RedirectUrl = collectUrl;
                ctx.DebitRefNum = handlerResponse.PaymentHandleToken;
            }
            else
            {
                integrationResult.Code = "000";
                integrationResult.Message = "Payment Processed";
            }
        }

        logger.SetShortMessage($"Status: {handlerResponse.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> CaptureTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(CaptureTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var settlementResult = await _apiClient.ProcessSettlementAsync(ctx, cancellationToken);

        logger.Info($"[ProcessSettlement] Code: {settlementResult.StatusCode} Path: {settlementResult.Path}");
        logger.Info($"[ProcessSettlement] Req: {settlementResult.RequestJson}");
        logger.Info($"[ProcessSettlement] Res: {settlementResult.ResponseJson}");

        if (settlementResult.Response is null)
            throw new Exception("Failed to parse settlement response", settlementResult.Exception);
        var response = settlementResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = response.Id;

        switch (response.Status?.ToUpper())
        {
            case "COMPLETED":
            case "INITIATED":
            case "PENDING":
            case "RECEIVED":
            case "PROCESSING":
                integrationResult.Code = "000";
                integrationResult.Message = "Amount Captured";
                break;
            default:
                integrationResult.Code = "99";
                integrationResult.Message = $"Capture status: {response.Status} {response.Error?.ToMessage()}";
                break;
        }

        logger.SetShortMessage($"Status: {response.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> VoidTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(VoidTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var voidResult = await _apiClient.VoidAuthorizationAsync(ctx, cancellationToken);

        logger.Info($"[VoidAuthorization] Code: {voidResult.StatusCode} Path: {voidResult.Path}");
        logger.Info($"[VoidAuthorization] Req: {voidResult.RequestJson}");
        logger.Info($"[VoidAuthorization] Res: {voidResult.ResponseJson}");

        if (voidResult.Response is null)
            throw new Exception("Failed to parse void response", voidResult.Exception);
        var response = voidResult.Response;

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = response.Id;

        switch (response.Status?.ToUpper())
        {
            case "COMPLETED":
            case "PENDING":
            case "RECEIVED":
            case "CANCELLED":
                integrationResult.Code = "000";
                integrationResult.Message = "Authorization Released";
                break;
            default:
                integrationResult.Code = "99";
                integrationResult.Message = "Authorization Release failed";
                break;
        }

        logger.SetShortMessage($"Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> RefundTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(RefundTrxAsync));
        ValidateDebitCompanyId(ctx, DebitCompanyId);

        var integrationResult = ctx.GetIntegrationResult();
        integrationResult.ApprovalNumber = ctx.ApprovalNumber;

        var refundResult = await _apiClient.ProcessRefundAsync(ctx, cancellationToken);

        logger.Info($"[ProcessRefund] Code: {refundResult.StatusCode} Path: {refundResult.Path}");
        logger.Info($"[ProcessRefund] Req: {refundResult.RequestJson}");
        logger.Info($"[ProcessRefund] Res: {refundResult.ResponseJson}");

        if (refundResult.Response is null)
            throw new Exception("Failed to parse refund response", refundResult.Exception);
        var response = refundResult.Response;

        switch (response.Status?.ToUpper())
        {
            case "COMPLETED":
                integrationResult.Code = "000";
                integrationResult.Message = "Refund Completed";
                break;
            case "INITIATED":
            case "PENDING":
            case "RECEIVED":
            case "PROCESSING":
                integrationResult.Code = "000";
                integrationResult.Message = "Refund Pending";
                break;
            default:
            {
                if (response.Error?.Code == "3406") // Settlement is not completed.
                {
                    var cancelResult = await _apiClient.CancelSettlementAsync(ctx, cancellationToken);
                    logger.Info($"[CancelSettlement] Code: {cancelResult.StatusCode} Path: {cancelResult.Path}");
                    logger.Info($"[CancelSettlement] Req: {cancelResult.RequestJson}");

                    if (cancelResult.StatusCode == 200)
                    {
                        integrationResult.Code = "000";
                        integrationResult.Message = "Refund Completed. Settlement was cancelled";
                        return integrationResult;
                    }
                }

                integrationResult.Code = "99";
                integrationResult.Message = $"Refund status: {response.Status} {response.Error?.ToMessage()}";
                break;
            }
        }

        logger.SetShortMessage($"Status: {response.Status} Code: {integrationResult.Code}");
        return integrationResult;
    }

    private async Task<IntegrationResult> FinalizeTrxAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        using var logger = _logger.GetScopedForIntegration(Tag, nameof(FinalizeTrxAsync));
        logger.Info($"Source: {(ctx.IsAutomatedRequest ? "Webhook" : "Redirect")}");

        try
        {
            if (!ctx.CheckFinalizeUrl() && !ctx.IsAutomatedRequest)
                return new IntegrationResult { Code = "500", Message = "Return URL signature mismatch" };

            var pendingTrx = await DataService.Transactions.GetPendingTrxByIdAsync(ctx.LocatedTrx.TrxId, cancellationToken);
            if (pendingTrx == null)
                throw new Exception($"Finalize pending trx '{ctx.LocatedTrx.TrxId}' not found");
            var log = await DataService.ChargeAttempts.GetByIdAsync(ctx.ChargeAttemptLogId);
            if (log is null)
                throw new Exception($"Charge attempt log not found for id '{ctx.ChargeAttemptLogId}'");

            var trxType = PaysafeServices.GetTrxType(ctx);
            var onlyAuthorize = trxType == PaysafeServices.TrxType.PreAuth;

            if (ctx.IsAutomatedRequest && !string.IsNullOrEmpty(ctx.AutomatedStatus) &&
                !string.IsNullOrEmpty(ctx.AutomatedCode))
            {
                logger.Info("AutoFinalize");
                return await AutoFinalizeTrxAsync(ctx, cancellationToken);
            }

            PaymentResponse? paymentResponse;
            if (ctx is { IsAutomatedRequest: true, AutomatedStatus: "PAYMENT_COMPLETED" })
            {
                logger.Info($"Automated Success. Status: {ctx.AutomatedStatus}");
                paymentResponse = ctx.AutomatedPayload as PaymentResponse;

                await DataService.ChargeAttempts.UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                    .SetInnerRequest($"Callback: {ctx.IsAutomatedRequest}\n{log.InnerRequest}\n{ctx.GetFinalizeUrl(ctx.FinalizeType)}")
                    .SetInnerResponse($"{ctx.AutomatedStatus}\n{log.InnerRequest}"), cancellationToken);
            }
            else
            {
                var paymentResult = await _apiClient.ProcessPaymentAsync(ctx, onlyAuthorize, cancellationToken);

                logger.Info($"[ProcessPayment] Code: {paymentResult.StatusCode} Path: {paymentResult.Path}");
                logger.Info($"[ProcessPayment] Req: {paymentResult.RequestJson}");
                logger.Info($"[ProcessPayment] Res: {paymentResult.ResponseJson}");

                paymentResponse = paymentResult.Response ??
                                  throw new Exception("Failed to parse payment response", paymentResult.Exception);

                await DataService.ChargeAttempts.UpdateAsync(id => id == ctx.ChargeAttemptLogId, u => u
                    .SetInnerRequest($"Callback: {ctx.IsAutomatedRequest}\n{log.InnerRequest}\n{ctx.GetFinalizeUrl(ctx.FinalizeType)}")
                    .SetInnerResponse($"{paymentResult.ResponseJson}\n{log.InnerRequest}"), cancellationToken);
            }

            if (paymentResponse is null)
                throw new Exception("Failed to parse payment response");

            // update ApprovalNumber with payment id
            if (!string.IsNullOrWhiteSpace(paymentResponse.Id))
            {
                ctx.ApprovalNumber = paymentResponse.Id;
                await DataService.Transactions.UpdatePendingTrxApprovalNumberAsync(ctx.LocatedTrx.TrxId,
                    paymentResponse.Id, cancellationToken);
            }

            var integrationResult = ctx.GetIntegrationResult();
            var status = paymentResponse.Status.NotNull();
            var errorMessage = paymentResponse.Error?.ToMessage();

            logger.Info($"Status: {status} TransType: {ctx.LocatedTrx?.TransType} Approval number: {ctx.ApprovalNumber}");

            // Decide reply code according to Paysafe status list
            var successStatuses = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "COMPLETED" };
            var pendingStatuses = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "RECEIVED", "PROCESSING", "PENDING", "HELD" };
            var isSuccess = successStatuses.Contains(status);
            var isPending = pendingStatuses.Contains(status);

            logger.Info($"Status: {status}");
            if (!string.IsNullOrEmpty(errorMessage)) logger.Info($"ErrorMessage: {errorMessage}");

            if (ctx.IsAutomatedRequest)
            {
                logger.Info("Automated request");
                if (isSuccess)
                {
                    integrationResult.Code = "000";
                    integrationResult.Message = "Payment Processed";
                    if (ctx.LocatedTrx?.TransType == 1) // pre-auth
                        integrationResult.TrxId = ctx.LocatedTrx.TrxId;
                }
                else if (isPending)
                {
                    integrationResult.Code = "001";
                    integrationResult.Message = "Payment Pending";
                }
                else
                {
                    integrationResult.Code = "99";
                    switch (paymentResponse.Error?.Code)
                    {
                        // Do not expose risk errors
                        case "4001":
                        case "4002":
                            logger.Warn($"Risk Error: {errorMessage}");
                            integrationResult.Message = "Payment Failed, An internal error occurred";
                            break;
                        default:
                            integrationResult.Message = "Payment Failed, " + errorMessage;
                            break;
                    }
                }
            }
            else
            {
                logger.Info($"Redirection request. Type: {ctx.FinalizeType}");
                switch (ctx.FinalizeType)
                {
                    case FinalizeUrlType.SuccessRedirect:
                        if (isSuccess)
                        {
                            integrationResult.Code = "000";
                            integrationResult.Message = "Payment Processed";
                            if (ctx.LocatedTrx?.TransType == 1) // pre-auth
                                integrationResult.TrxId = ctx.LocatedTrx.TrxId;
                        }
                        else if (isPending)
                        {
                            integrationResult.Code = "553"; // redirect pending
                            integrationResult.Message = "Payment Pending";
                        }
                        else
                        {
                            integrationResult.Code = "99";
                            integrationResult.Message = "Payment Failed, " + errorMessage;
                        }

                        break;
                    case FinalizeUrlType.FailureRedirect:
                        integrationResult.Code = "99";
                        integrationResult.Message = "Payment Failed, " + errorMessage;
                        break;
                    default:
                        integrationResult.Code = "99";
                        integrationResult.Message = "Unknown finalize type";
                        break;
                }
            }

            await CompleteFinalizationAsync(ctx, integrationResult, cancellationToken);

            logger.SetShortMessage($"Status: {status} Approval Number: {integrationResult.ApprovalNumber} Pass/Fail Id: {integrationResult.TrxId} Code: {integrationResult.Code}");

            return integrationResult;
        }
        catch (Exception e)
        {
            logger.Error($"Error in Paysafe finalization: {e.Message}\n\n{e.StackTrace}");
            return new IntegrationResult { Code = "500", Message = "Internal server error: finalization" };
        }
    }
}