using Ezzygate.Integrations.Core.Processing;
using Ezzygate.WebApi.Dtos;

namespace Ezzygate.WebApi.Extensions;

public static class DtoMappingExtensions
{
    public static ProcessRequest ToProcessRequest(this IntegrationProcessRequestDto dto)
    {
        return new ProcessRequest
        {
            OperationType = dto.OperationType,
            DebitRefCode = dto.DebitRefCode,
            DebitRefNum = dto.DebitRefNum,
            ApprovalNumber = dto.ApprovalNumber,
            TerminalId = dto.TerminalId,
            Is3DSecure = dto.Is3DSecure,
            CreditCard = dto.CreditCard,
            Customer = dto.Customer,
            PaymentMethodId = dto.PaymentMethodId,
            Amount = dto.Amount,
            OriginalAmount = dto.OriginalAmount,
            CurrencyIso = dto.CurrencyIso,
            Payments = dto.Payments,
            TransType = dto.TransType,
            CreditType = dto.CreditType,
            ChargeAttemptLogId = dto.ChargeAttemptLogId,
            MerchantNumber = dto.MerchantNumber,
            OrderId = dto.OrderId,
            CartId = dto.CartId,
            CustomerId = dto.CustomerId,
            Comment = dto.Comment,
            RoutingNumber = dto.RoutingNumber,
            AccountNumber = dto.AccountNumber,
            AccountName = dto.AccountName,
            ClientIp = dto.ClientIp,
            RequestContent = dto.RequestContent,
            FormData = dto.FormData,
            QueryString = dto.QueryString,
            RequestSource = dto.RequestSource
        };
    }

    public static FinalizeRequest ToFinalizeRequest(this IntegrationFinalizeRequestDto dto)
    {
        return new FinalizeRequest
        {
            DebitReferenceCode = dto.DebitRefCode ?? string.Empty,
            DebitReferenceNum = dto.DebitRefNum,
            ChargeAttemptLogId = dto.ChargeAttemptLogId,
            OperationType = dto.OperationType,
            RequestContent = dto.RequestContent,
            FormData = dto.FormData,
            QueryString = dto.QueryString
        };
    }
}