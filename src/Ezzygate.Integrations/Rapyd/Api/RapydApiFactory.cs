using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api;

public static class RapydApiFactory
{
    public static PaymentRequest CreateProcessRequest(TransactionContext ctx, bool capture, string methodType)
    {
        var request = new PaymentRequest
        { 
            Amount = ctx.Amount.ToString("0.00"),
            Currency = ctx.CurrencyIso,
            PaymentMethod = new CardPaymentMethod
            {
                Type = methodType,
                PaymentFields = new CardPaymentFields
                {
                    Number = ctx.CardNumber,
                    ExpirationMonth = ctx.Expiration.MM,
                    ExpirationYear = ctx.Expiration.YY,
                    Cvv = ctx.Cvv,
                    Name = ctx.PayerName
                }
            },
            Capture = capture,
            ErrorPaymentUrl = ctx.GetFinalizeUrl(FinalizeUrlType.FailureRedirect),
            CompletePaymentUrl = ctx.GetFinalizeUrl(FinalizeUrlType.SuccessRedirect)
        };

        return request;
    }

    public static RefundRequest CreateRefundRequest(TransactionContext ctx)
    {
        var request = new RefundRequest
        { 
            Payment = ctx.ApprovalNumber,
            Metadata = new Metadata
            {
                MerchantDefined = true
            },
            MerchantReferenceId = ctx.MerchantNumber,
            Reason = ctx.Comment
        };

        return request;
    }

    public static CaptureRequest CreateCaptureRequest(TransactionContext ctx)
    {
        var request = new CaptureRequest
        { 
            Amount = ctx.Amount.ToString("0.00"),
            Email = ctx.Email,
            StatementDescriptor = ctx.Comment.Truncate(22),
        };

        return request;
    }
}