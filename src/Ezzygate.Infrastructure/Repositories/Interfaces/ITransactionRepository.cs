using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string message, string? binCountryIso);
    Task<PendingTransaction?> GetPendingTrxAsync(int approvalId);
    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalId);
    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType);
}