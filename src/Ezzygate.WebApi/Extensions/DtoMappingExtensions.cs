using Ezzygate.Integrations.Services.Processing;
using Ezzygate.WebApi.Models.Integration;

namespace Ezzygate.WebApi.Extensions;

public static class DtoMappingExtensions
{
    public static ProcessRequest ToProcessRequest(this IntegrationProcessRequestDto dto)
    {
        return new ProcessRequest
        {
            OperationType = dto.OperationType,
            TerminalId = dto.TerminalId,
            PaymentMethodId = dto.PaymentMethodId,
            DebitRefCode = dto.DebitRefCode,
            DebitRefNum = dto.DebitRefNum,
            ApprovalNumber = dto.ApprovalNumber,
            Amount = dto.Amount,
            OriginalAmount = dto.OriginalAmount,
            CurrencyIso = dto.CurrencyIso,
            Payments = dto.Payments,
            TransType = dto.TransType,
            CreditType = dto.CreditType,
            CardHolderName = dto.CreditCard?.HolderName,
            CardNumber = dto.CreditCard?.Number,
            Cvv = dto.CreditCard?.Cvv,
            Track2 = dto.CreditCard?.Track2,
            ExpirationMonth = dto.CreditCard?.ExpirationMonth ?? 0,
            ExpirationYear = dto.CreditCard?.ExpirationYear ?? 0,
            Email = dto.Customer?.Email,
            PersonalIdNumber = dto.Customer?.PersonalIdNumber,
            PhoneNumber = dto.Customer?.PhoneNumber,
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
            RequestSource = dto.RequestSource,
            IsAutomatedRequest = dto.IsAutomatedRequest,
            AutomatedStatus = dto.AutomatedStatus,
            AutomatedErrorMessage = dto.AutomatedErrorMessage,
            AutomatedPayload = dto.AutomatedPayload
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
            QueryString = dto.QueryString,
            IsAutomatedRequest = dto.IsAutomatedRequest,
            AutomatedStatus = dto.AutomatedStatus,
            AutomatedErrorMessage = dto.AutomatedErrorMessage,
            AutomatedPayload = dto.AutomatedPayload
        };
    }
}