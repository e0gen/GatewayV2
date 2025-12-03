namespace Ezzygate.Application.Transactions;

public record PendingFinalizeInfo(
    int PendingId,
    DateTime FinalizeDate,
    int? TransPassId,
    int? TransFailId,
    int? TransApprovalId);