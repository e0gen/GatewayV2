using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Abstractions;

namespace Ezzygate.Integrations.Core.Processing;

public sealed class IntegrationProcessor : IIntegrationProcessor
{
    private readonly ILogger<IntegrationProcessor> _logger;
    private readonly ITransactionContextFactory _transactionContextFactory;
    private readonly ICreditCardIntegrationProcessor _creditCardProcessor;
    private readonly IOptions<IntegrationSettings> _integrationSettings;

    public IntegrationProcessor(
        ILogger<IntegrationProcessor> logger,
        ITransactionContextFactory transactionContextFactory,
        ICreditCardIntegrationProcessor creditCardProcessor,
        IOptions<IntegrationSettings> integrationSettings)
    {
        _logger = logger;
        _transactionContextFactory = transactionContextFactory;
        _creditCardProcessor = creditCardProcessor;
        _integrationSettings = integrationSettings;
    }

    public async Task<IntegrationResult> ProcessAsync(ProcessRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.Info(LogTag.Integration, "Processing integration request: TerminalId={TerminalId}, OpType={OpType}",
                request.TerminalId, request.OperationType);

            var ctx = await _transactionContextFactory.CreateAsync(request.TerminalId, request.PaymentMethodId);

            ctx.OpType = request.OperationType;
            ctx.SentDebitRefCode = request.DebitRefCode;
            ctx.ApprovalNumber = request.ApprovalNumber;
            ctx.DebitRefNum = request.DebitRefNum ?? string.Empty;
            ctx.Amount = request.Amount;
            ctx.OriginalAmount = request.OriginalAmount;
            ctx.CurrencyIso = request.CurrencyIso;
            ctx.Payments = request.Payments;
            ctx.TransType = request.TransType;
            ctx.CreditType = request.CreditType;
            ctx.PayerName = request.CardHolderName ?? string.Empty;
            ctx.CardNumber = request.CardNumber ?? string.Empty;
            ctx.Cvv = request.Cvv ?? string.Empty;
            ctx.Track2 = request.Track2 ?? string.Empty;
            ctx.ExpirationMonth = request.ExpirationMonth;
            ctx.ExpirationYear = request.ExpirationYear;
            ctx.Email = request.Email ?? string.Empty;
            ctx.PersonalIdNumber = request.PersonalIdNumber ?? string.Empty;
            ctx.PhoneNumber = request.PhoneNumber ?? string.Empty;
            ctx.MerchantNumber = request.MerchantNumber;
            ctx.OrderId = request.OrderId;
            ctx.CartId = request.CartId;
            ctx.CustomerId = request.CustomerId;
            ctx.Comment = request.Comment;
            ctx.RoutingNumber = request.RoutingNumber;
            ctx.AccountNumber = request.AccountNumber;
            ctx.AccountName = request.AccountName;
            ctx.ClientIp = request.ClientIp;
            ctx.RequestContent = request.RequestContent;
            ctx.FormData = request.FormData;
            ctx.QueryString = request.QueryString;
            ctx.RequestSource = request.RequestSource;
            ctx.IsAutomatedRequest = request.IsAutomatedRequest;
            ctx.AutomatedStatus = request.AutomatedStatus;
            ctx.AutomatedCode = request.AutomatedCode;
            ctx.AutomatedMessage = request.AutomatedErrorMessage;
            ctx.AutomatedPayload = request.AutomatedPayload;
            if (!string.IsNullOrEmpty(ctx.QueryString))
            {
                var queryParams = QueryHelpers.ParseQuery(ctx.QueryString);
                if (queryParams.TryGetValue("l3d_arrival_date", out var l3dArrivalDate))
                    ctx.Level3DataArrivalDate = l3dArrivalDate.ToString();
            }

            ctx.IsMobileMoto = !string.IsNullOrEmpty(request.Comment) &&
                               request.Comment.StartsWith("fcm") &&
                               ctx.RequestSource == TransactionSource.WebApi;

            var result = await _creditCardProcessor.ProcessTransactionAsync(ctx, cancellationToken);

            if (_integrationSettings.Value.DisablePostRedirectUrl)
                result.RedirectUrl = null;

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Process exception: OpType={OperationType}", request.OperationType);
            return new IntegrationResult
            {
                Code = "520",
                Message = "[002] Internal server error, see logs",
                DebitRefCode = request.DebitRefCode
            };
        }
    }
}