using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ezzygate.Application.Integrations;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
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

            var ctx = await _transactionContextFactory.CreateAsync(request.TerminalId);

            ctx.OpType = request.OperationType;
            ctx.Amount = request.Amount;
            ctx.OriginalAmount = request.OriginalAmount;
            ctx.CurrencyIso = request.CurrencyIso;

            ctx.BillingAddress = new Address
            {
                AddressLine1 = request.CreditCard?.BillingAddress?.AddressLine1,
                AddressLine2 = request.CreditCard?.BillingAddress?.AddressLine2,
                City = request.CreditCard?.BillingAddress?.City,
                CountryIsoCode = request.CreditCard?.BillingAddress?.CountryIso,
                PostalCode = request.CreditCard?.BillingAddress?.PostalCode,
                StateIsoCode = request.CreditCard?.BillingAddress?.StateIso
            };
            ctx.CardNumber = request.CreditCard?.Number;
            ctx.Cvv = request.CreditCard?.Cvv;
            ctx.Track2 = request.CreditCard?.Track2;
            ctx.ExpirationMonth = request.CreditCard?.ExpirationMonth ?? 0;
            ctx.ExpirationYear = request.CreditCard?.ExpirationYear ?? 0;
            ctx.PayerName = request.CreditCard?.HolderName;
            
            ctx.Email = request.Customer?.Email;
            ctx.PersonalIdNumber = request.Customer?.PersonalIdNumber;
            ctx.PhoneNumber = request.Customer?.PhoneNumber;
            ctx.DateOfBirth = request.Customer?.DateOfBirth;

            ctx.Payments = request.Payments;
            ctx.DebitRefCode = Guid.NewGuid().ToString();
            ctx.SentDebitRefCode = request.DebitRefCode;
            ctx.ApprovalNumber = request.ApprovalNumber;
            ctx.DebitRefNum = request.DebitRefNum;
            ctx.RequestContent = request.RequestContent;
            ctx.FormData = request.FormData;
            ctx.QueryString = request.QueryString;
            ctx.RequestSource = request.RequestSource;
            ctx.ClientIp = request.ClientIp;

            ctx.MerchantNumber = request.MerchantNumber;
            ctx.OrderId = request.OrderId;
            ctx.CartId = request.CartId;
            ctx.CustomerId = request.CustomerId;
            ctx.ChargeAttemptLogId = request.ChargeAttemptLogId;
            ctx.TransType = request.TransType;
            ctx.CreditType = request.CreditType;
            ctx.Comment = request.Comment;
            ctx.RoutingNumber = request.RoutingNumber;
            ctx.AccountNumber = request.AccountNumber;
            ctx.AccountName = request.AccountName;
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