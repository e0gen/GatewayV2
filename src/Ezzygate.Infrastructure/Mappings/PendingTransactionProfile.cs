using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class PendingTransactionProfile
{
    public static PendingTransaction ToDomain(this TblCompanyTransPending entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new PendingTransaction
        {
            Id = entity.Id,
            CompanyTransPendingId = entity.CompanyTransPendingId,
            CompanyId = entity.CompanyId ?? 0,
            DebitCompanyId = entity.DebitCompanyId,
            TransType = entity.TransType,
            TransAmount = entity.TransAmount,
            TransCreditType = entity.TransCreditType,
            TransPayments = entity.TransPayments,
            Currency = entity.Currency,
            TransSourceId = entity.TransSourceId,
            PayForText = entity.PayforText,
            CustomerId = entity.CustomerId,
            PaymentMethodDisplay = entity.PaymentMethodDisplay,
            Ipaddress = entity.Ipaddress,
            TerminalNumber = entity.TerminalNumber,
            DebitReferenceCode = entity.DebitReferenceCode,
            DebitReferenceNum = entity.DebitReferenceNum,
            TransOrder = entity.TransOrder,
            DebitApprovalNumber = entity.DebitApprovalNumber,
            OrderNumber = entity.OrderNumber,
            TransPayerInfoId = entity.TransPayerInfoId,
            CreditCardId = entity.CreditCardId,
            PaymentMethod = entity.PaymentMethod,
            TransPaymentMethodId = entity.TransPaymentMethodId,
            IsTestOnly = entity.IsTestOnly,
            Comment = entity.Comment
        };
    }
}