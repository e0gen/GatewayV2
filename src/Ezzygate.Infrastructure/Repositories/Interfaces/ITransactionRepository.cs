using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task UpdateChargeAttemptAsync(int pendingTrxId, int movedTrxId, string replyCode, string errorMessage);

    Task<MoveTransactionResult> MoveTrxAsync(int pendingId,
        string replyCode, string message, string? binCountryIso);

    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalId);

    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType);
}