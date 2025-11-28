using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Paysafe.Models;

namespace Ezzygate.Integrations.Paysafe.Api;

public static class PaysafeApiFactory
{
    public static CreatePaymentHandlerRequest CreatePaymentHandlerRequest(TransactionContext ctx)
    {
        var accountNumber = PaysafeServices.GetAccountNumber(ctx.CurrencyIso, ctx.IsTestTerminal);

        var request = new CreatePaymentHandlerRequest
        {
            MerchantRefNum = ctx.DebitRefCode,
            TransactionType = "PAYMENT",
            ThreeDs = new ThreeDs
            {
                MerchantUrl = ctx.GetMerchantUrl(),
                DeviceChannel = "BROWSER",
                MessageCategory = "PAYMENT",
                AuthenticationPurpose = "PAYMENT_TRANSACTION",
            },
            Profile = new Profile
            {
                FirstName = ctx.PayerFirstName,
                LastName = ctx.PayerLastName,
                Email = ctx.Email,
                Phone = ctx.PhoneNumber
            },
            Card = new Card
            {
                CardNum = ctx.CardNumber,
                CardExpiry = new CardExpiry
                {
                    Month = ctx.ExpirationMonth,
                    Year = ctx.ExpirationYear
                },
                Cvv = ctx.Cvv,
                HolderName = ctx.PayerName
            },
            PaymentType = "CARD",
            Amount = ctx.AmountInteger,
            CurrencyCode = ctx.CurrencyIso
        };

        if (!string.IsNullOrEmpty(accountNumber))
            request.AccountId = accountNumber;
        if (!string.IsNullOrEmpty(ctx.BillingAddress.CountryIsoCode) &&
            !string.IsNullOrEmpty(ctx.BillingAddress.PostalCode))
            request.BillingDetails = new BillingDetails
            {
                Street = ctx.BillingAddress.AddressLine1,
                City = ctx.BillingAddress.City,
                State = ctx.BillingAddress.StateIsoCode,
                Country = ctx.BillingAddress.CountryIsoCode,
                Zip = ctx.BillingAddress.PostalCode
            };

        var defaultUrl = ctx.GetFinalizeUrl(FinalizeUrlType.GeneralRedirect);
        var successUrl = ctx.GetFinalizeUrl(FinalizeUrlType.SuccessRedirect);
        var failureUrl = ctx.GetFinalizeUrl(FinalizeUrlType.FailureRedirect);

        request.ReturnLinks =
        [
            new Link { Rel = "default", Method = "GET", Href = defaultUrl },
            new Link { Rel = "on_completed", Method = "GET", Href = successUrl },
            new Link { Rel = "on_failed", Method = "GET", Href = failureUrl }
        ];

        return request;
    }

    public static PaymentRequest CreatePaymentRequest(TransactionContext ctx, bool onlyAuthorize)
    {
        return new PaymentRequest
        {
            MerchantRefNum = ctx.DebitRefCode,
            Amount = ctx.AmountInteger,
            CurrencyCode = ctx.CurrencyIso,
            DupCheck = false,
            SettleWithAuth = !onlyAuthorize,
            PaymentHandleToken = ctx.DebitRefNum.NotNull(),
            CustomerIp = ctx.ClientIp,
            Description = ctx.PayFor,
            Keywords = []
        };
    }

    public static SettlementRequest CreateSettlementRequest(TransactionContext ctx)
    {
        return new SettlementRequest
        {
            MerchantRefNum = ctx.DebitRefCode,
            Amount = ctx.AmountInteger,
            DupCheck = false
        };
    }

    public static RefundRequest CreateRefundRequest(TransactionContext ctx)
    {
        return new RefundRequest
        {
            MerchantRefNum = ctx.DebitRefCode,
            Amount = ctx.AmountInteger,
            DupCheck = false
        };
    }

    public static CancelSettlementRequest CreateCancelSettlementRequest()
    {
        return new CancelSettlementRequest();
    }

    public static VoidAuthorizationRequest CreateVoidAuthorizationRequest(TransactionContext ctx)
    {
        return new VoidAuthorizationRequest
        {
            MerchantRefNum = ctx.DebitRefCode,
            Amount = ctx.AmountInteger
        };
    }
}