using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class TransactionSearchResultProfile
{
    public static TransactionSearchResult ToSearchResult(this TblCompanyTransPass trx)
    {
        return new TransactionSearchResult(
            Id: trx.Id,
            Status: "Captured",
            Amount: trx.Amount,
            ApprovalCode: trx.ApprovalNumber,
            PaymentMethodDisplay: trx.PaymentMethodDisplay,
            InsertDate: trx.InsertDate,
            CurrencyIso: trx.CurrencyNavigation?.CurIsoname,
            Installments: trx.Payments,
            Comment: trx.Comment,
            ResponseCode: trx.ReplyCode,
            ResponseMessage: null,
            DebitReferenceCode: trx.DebitReferenceCode,
            OrderNumber: trx.OrderNumber,
            PayerFirstName: trx.TransPayerInfo?.FirstName,
            PayerLastName: trx.TransPayerInfo?.LastName,
            PayerPersonalNumber: trx.TransPayerInfo?.PersonalNumber,
            PayerEmail: trx.TransPayerInfo?.EmailAddress,
            PayerPhone: trx.TransPayerInfo?.PhoneNumber);
    }

    public static TransactionSearchResult ToSearchResult(this TblCompanyTransFail trx)
    {
        return new TransactionSearchResult(
            Id: trx.Id,
            Status: "Declined",
            Amount: trx.Amount,
            ApprovalCode: trx.ApprovalNumber,
            PaymentMethodDisplay: trx.PaymentMethodDisplay,
            InsertDate: trx.InsertDate,
            CurrencyIso: trx.CurrencyNavigation?.CurIsoname,
            Installments: trx.Payments,
            Comment: trx.Comment,
            ResponseCode: trx.ReplyCode,
            ResponseMessage: trx.DebitDeclineReason,
            DebitReferenceCode: trx.DebitReferenceCode,
            OrderNumber: trx.OrderNumber,
            PayerFirstName: trx.TransPayerInfo?.FirstName,
            PayerLastName: trx.TransPayerInfo?.LastName,
            PayerPersonalNumber: trx.TransPayerInfo?.PersonalNumber,
            PayerEmail: trx.TransPayerInfo?.EmailAddress,
            PayerPhone: trx.TransPayerInfo?.PhoneNumber);
    }

    public static TransactionSearchResult ToSearchResult(this TblCompanyTransApproval trx)
    {
        return new TransactionSearchResult(
            Id: trx.Id,
            Status: "Authorized",
            Amount: trx.Amount,
            ApprovalCode: trx.ApprovalNumber,
            PaymentMethodDisplay: trx.PaymentMethodDisplay,
            InsertDate: trx.InsertDate,
            CurrencyIso: trx.CurrencyNavigation?.CurIsoname,
            Installments: trx.Payments,
            Comment: trx.Comment,
            ResponseCode: trx.ReplyCode,
            ResponseMessage: null,
            DebitReferenceCode: trx.DebitReferenceCode,
            OrderNumber: trx.OrderNumber,
            PayerFirstName: trx.TransPayerInfo?.FirstName,
            PayerLastName: trx.TransPayerInfo?.LastName,
            PayerPersonalNumber: trx.TransPayerInfo?.PersonalNumber,
            PayerEmail: trx.TransPayerInfo?.EmailAddress,
            PayerPhone: trx.TransPayerInfo?.PhoneNumber);
    }

    public static TransactionSearchResult ToSearchResult(this TblCompanyTransPending trx)
    {
        return new TransactionSearchResult(
            Id: trx.Id,
            Status: "Pending",
            Amount: trx.TransAmount,
            ApprovalCode: trx.DebitApprovalNumber,
            PaymentMethodDisplay: trx.PaymentMethodDisplay,
            InsertDate: trx.InsertDate,
            CurrencyIso: trx.TransCurrencyNavigation?.CurIsoname,
            Installments: trx.TransPayments,
            Comment: trx.Comment,
            ResponseCode: trx.ReplyCode,
            ResponseMessage: null,
            DebitReferenceCode: trx.DebitReferenceCode,
            OrderNumber: trx.TransOrder,
            PayerFirstName: trx.TransPayerInfo?.FirstName,
            PayerLastName: trx.TransPayerInfo?.LastName,
            PayerPersonalNumber: trx.TransPayerInfo?.PersonalNumber,
            PayerEmail: trx.TransPayerInfo?.EmailAddress,
            PayerPhone: trx.TransPayerInfo?.PhoneNumber);
    }
}