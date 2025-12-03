using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Ef.Entities;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId);
    Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId);
    Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber);
    Task<PendingFinalizeInfo?> GetPendingFinalizeInfoAsync(int pendingTrxId);
    Task<TblCompanyTransPending?> GetPendingTrxEntityAsync(int pendingTrxId);
    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType);
    Task UpdatePendingTrxApprovalNumberAsync(int pendingTrxId, string approvalNumber);
    Task UpdateCartForFinalizedTrxAsync(int pendingId, int? passId, int? approvalId);
    Task AddPendingFinalizeAsync(PendingFinalizeInfo finalizeInfo);
    Task<int> AddPassedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode, string? binCountryIso);
    Task<int> AddApprovedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode, string? binCountryIso);
    Task<int> AddFailTrxAsync(TblCompanyTransPending pendingTrx, string replyCode, string message, string? binCountryIso);
    Task RemovePendingTrxAsync(TblCompanyTransPending pendingTrx);
}