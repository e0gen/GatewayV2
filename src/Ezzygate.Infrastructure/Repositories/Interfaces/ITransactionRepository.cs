using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId);
    Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId);
    Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber);
    Task<PendingFinalizeInfo?> GetPendingFinalizeInfoAsync(int pendingTrxId);
    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType);
    Task UpdatePendingTrxApprovalNumberAsync(int pendingTrxId, string approvalNumber);

    Task<List<TransactionSearchResult>> SearchTransactionsAsync(int merchantId, TransactionStatusType status, int? transactionId = null, DateTime? from = null, DateTime? to = null);
    Task<List<TransactionSearchResult>> SearchCapturedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchDeclinedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchAuthorizedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
    Task<List<TransactionSearchResult>> SearchPendingTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to);
}