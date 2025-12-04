using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Processing.Models;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.WebApi.Dtos.Merchants.CreditCard;

public static class CreditCardDtoFactory
{
    public static LegacyProcessRequest CreateLegacyProcessRequest(CreditcardProcessRequestDto dto, Merchant merchant)
    {
        var isStorePm = dto.SaveCard ? 1 : 0;
        var transType = dto.AuthorizeOnly ? 1 : 0;
        if (!string.IsNullOrWhiteSpace(dto.SavedCardId))
            transType = 3;

        var typeCredit = dto.Installments > 1 ? 8 : 1;

        return new LegacyProcessRequest
        {
            MerchantNumber = merchant.CustomerNumber,
            CreditCard = dto.CreditCard != null ? new LegacyCreditCard
            {
                Number = dto.CreditCard.Number,
                ExpirationMonth = dto.CreditCard.ExpirationMonth,
                ExpirationYear = dto.CreditCard.ExpirationYear,
                HolderName = dto.CreditCard.HolderName,
                Cvv = dto.CreditCard.Cvv,
                Type = dto.CreditCard.Type,
                BillingAddress = dto.CreditCard.BillingAddress != null ? new LegacyBillingAddress
                {
                    AddressLine1 = dto.CreditCard.BillingAddress.AddressLine1,
                    AddressLine2 = dto.CreditCard.BillingAddress.AddressLine2,
                    City = dto.CreditCard.BillingAddress.City,
                    PostalCode = dto.CreditCard.BillingAddress.PostalCode,
                    StateIso = dto.CreditCard.BillingAddress.StateIso,
                    CountryIso = dto.CreditCard.BillingAddress.CountryIso
                } : null
            } : null,
            Customer = dto.Customer != null ? new LegacyCustomer
            {
                FullName = dto.Customer.FullName,
                PhoneNumber = dto.Customer.PhoneNumber,
                PersonalIdNumber = dto.Customer.PersonalIdNumber,
                Email = dto.Customer.Email,
                DateOfBirth = dto.Customer.DateOfBirth
            } : null,
            Recurring = dto.Recurring != null ? new LegacyRecurring
            {
                Recurring1 = dto.Recurring.Recurring1,
                Recurring2 = dto.Recurring.Recurring2,
                Recurring3 = dto.Recurring.Recurring3,
                Recurring4 = dto.Recurring.Recurring4,
                Recurring5 = dto.Recurring.Recurring5,
                Recurring6 = dto.Recurring.Recurring6
            } : null,
            Level3Data = dto.Level3Data != null ? new LegacyLevel3Data
            {
                ArrivalDate = dto.Level3Data.ArrivalDate
            } : null,
            Amount = dto.Amount,
            Installments = dto.Installments,
            CurrencyIso = dto.CurrencyIso,
            TransType = transType,
            TypeCredit = typeCredit,
            PostRedirectUrl = dto.PostRedirectUrl,
            StoreCc = isStorePm,
            SavedCardId = dto.SavedCardId,
            TrmCode = dto.TrmCode,
            Order = dto.Order,
            Comment = dto.Comment,
            OrderDescription = dto.OrderDescription,
            ClientIP = dto.ClientIP,
            TipAmount = dto.TipAmount,
            CardPresent = dto.CardPresent
        };
    }

    public static LegacyRefundRequest CreateLegacyRefundRequest(RefundRequestDto dto, Merchant merchant, string clientIp)
    {
        return new LegacyRefundRequest
        {
            MerchantNumber = merchant.CustomerNumber,
            Amount = dto.Amount,
            CurrencyIso = dto.CurrencyIso,
            TransactionId = dto.TransactionId,
            ClientIP = clientIp
        };
    }

    public static LegacyVoidRequest CreateLegacyVoidRequest(VoidRequestDto dto, Merchant merchant, string clientIp)
    {
        return new LegacyVoidRequest
        {
            MerchantNumber = merchant.CustomerNumber,
            CurrencyIso = dto.CurrencyIso,
            TransactionId = dto.TransactionId,
            ClientIP = clientIp
        };
    }

    public static LegacyCaptureRequest CreateLegacyCaptureRequest(CaptureRequestDto dto, Merchant merchant, string clientIp)
    {
        return new LegacyCaptureRequest
        {
            MerchantNumber = merchant.CustomerNumber,
            CurrencyIso = dto.CurrencyIso,
            TransactionId = dto.TransactionId,
            ClientIP = clientIp
        };
    }

    public static CreditcardProcessResponseDto CreateBasicResponse(LegacyPaymentResult result, ICurrencyRepository currencyRepository)
    {
        var currency = currencyRepository.Get(int.Parse(result.Currency));

        return new CreditcardProcessResponseDto
        {
            CreditCard = new CreditCardDto
            {
                Number = result.Last4,
                Type = result.CardType,
                Cvv = "..."
            },
            Amount = decimal.Parse(result.Amount),
            CurrencyIso = currency.IsoCode,
            ReplyCode = result.ReplyCode,
            TransactionId = result.TransactionId,
            ReplyDescription = result.ReplyDescription,
            SavedCardId = result.SavedCardId,
            Descriptor = result.Descriptor,
            AuthenticationRedirectUrl = result.AuthenticationRedirectUrl,
            RecurringSeriesId = result.RecurringSeries
        };
    }

    public static CreditcardProcessResponseDto CreateExtendedResponse(
        LegacyPaymentResult result,
        CreditcardProcessRequestDto request,
        Merchant merchant,
        ICurrencyRepository currencyRepository)
    {
        var currency = currencyRepository.Get(int.Parse(result.Currency));
        var transType = request.AuthorizeOnly ? 1 : 0;
        if (!string.IsNullOrWhiteSpace(request.SavedCardId))
            transType = 3;

        return new CreditcardProcessResponseDto
        {
            CreditCard = new CreditCardDto
            {
                Number = result.Last4,
                Type = result.CardType,
                Cvv = "..."
            },
            Amount = decimal.Parse(result.Amount),
            CurrencyIso = currency.IsoCode,
            ReplyCode = result.ReplyCode,
            TransactionId = result.TransactionId,
            ReplyDescription = result.ReplyDescription,
            SavedCardId = result.SavedCardId,
            Descriptor = result.Descriptor,
            AuthenticationRedirectUrl = result.AuthenticationRedirectUrl,
            RecurringSeriesId = result.RecurringSeries,
            TransType = transType,
            Order = result.Order,
            Comment = result.Comment,
            Date = result.Date,
            ConfirmationNumber = result.ConfirmationNumber,
            MerchantID = merchant.CustomerNumber,
            ClientEmail = request.Customer?.Email,
            ClientPhoneNumber = request.Customer?.PhoneNumber,
            ClientFullName = request.Customer?.FullName,
            ClientWalletId = result.WalletId,
            ApprovalNumber = result.ApprovalNumber ?? string.Empty,
            AcquirerReferenceNumber = result.AcquirerReferenceNumber ?? string.Empty,
            ClientId = string.Empty,
            TransRefNum = result.TransRefNum ?? string.Empty,
            Signature = result.Signature,
            TransMaxInstallments = result.Payments,
            DispRecurring = string.Empty,
            DispMobile = string.Empty,
            DebugTest = string.Empty
        };
    }

    public static RefundResponseDto CreateRefundResponse(LegacyPaymentResult result, ICurrencyRepository currencyRepository)
    {
        var currency = currencyRepository.Get(int.Parse(result.Currency));

        return new RefundResponseDto
        {
            Amount = decimal.Parse(result.Amount),
            CurrencyIso = currency.IsoCode,
            ReplyCode = result.ReplyCode,
            TransactionId = int.Parse(result.TransactionId),
            ReplyDescription = result.ReplyDescription
        };
    }

    public static VoidResponseDto CreateVoidResponse(LegacyPaymentResult result, ICurrencyRepository currencyRepository)
    {
        var currency = currencyRepository.Get(int.Parse(result.Currency));

        return new VoidResponseDto
        {
            CurrencyIso = currency.IsoCode,
            ReplyCode = result.ReplyCode,
            TransactionId = result.TransactionId,
            ReplyDescription = result.ReplyDescription
        };
    }

    public static CaptureResponseDto CreateCaptureResponse(LegacyPaymentResult result, ICurrencyRepository currencyRepository)
    {
        var currency = currencyRepository.Get(int.Parse(result.Currency));

        return new CaptureResponseDto
        {
            CurrencyIso = currency.IsoCode,
            ReplyCode = result.ReplyCode,
            TransactionId = result.TransactionId,
            ReplyDescription = result.ReplyDescription
        };
    }
}