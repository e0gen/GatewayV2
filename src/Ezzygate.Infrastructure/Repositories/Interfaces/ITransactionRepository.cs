using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string message, string? binCountryIso);
    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId);
    Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId);
    Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber);
    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType);
}