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

    Task<List<TransactionSearchResult>> SearchTransactionsAsync(int merchantId, TransactionStatusType status, int? transactionId = null, DateTime? from = null, DateTime? to = null);
    Task<List<TransactionSearchResult>> SearchCapturedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchDeclinedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchAuthorizedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchPendingTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
}