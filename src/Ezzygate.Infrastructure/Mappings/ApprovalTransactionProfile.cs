using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class ApprovalTransactionProfile
{
    public static ApprovalTransaction ToDomain(this TblCompanyTransApproval entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ApprovalTransaction
        {
            Id = entity.Id,
            AuthStatus = entity.AuthStatus
        };
    }
}