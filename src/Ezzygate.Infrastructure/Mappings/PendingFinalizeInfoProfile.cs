using Ezzygate.Application.Transactions;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Mappings;

public static class PendingFinalizeInfoProfile
{
    public static PendingFinalizeInfo ToDomain(this TblLogPendingFinalize entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new PendingFinalizeInfo(
            entity.PendingId,
            entity.FinalizeDate,
            entity.TransPassId,
            entity.TransFailId,
            entity.TransApprovalId);
    }

    public static TblLogPendingFinalize ToEntity(this PendingFinalizeInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);

        return new TblLogPendingFinalize
        {
            PendingId = info.PendingId,
            FinalizeDate = info.FinalizeDate,
            TransPassId = info.TransPassId,
            TransFailId = info.TransFailId,
            TransApprovalId = info.TransApprovalId
        };
    }
}