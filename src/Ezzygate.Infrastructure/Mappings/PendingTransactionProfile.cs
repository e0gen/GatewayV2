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
            TransType = entity.TransType
        };
    }
}