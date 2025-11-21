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
            CompanyId = entity.CompanyId.Value,
            TransType = entity.TransType,
            TransAmount = entity.TransAmount,
            Currency = entity.Currency,
            OrderNumber = entity.OrderNumber,
            TransPayments = entity.TransPayments,
            Comment = entity.Comment,
            CompanyTransPendingId = entity.CompanyTransPendingId,
            DebitApprovalNumber = entity.DebitApprovalNumber,
        };
    }
}