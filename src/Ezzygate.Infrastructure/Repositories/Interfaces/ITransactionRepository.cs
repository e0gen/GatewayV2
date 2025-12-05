using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;

namespace Ezzygate.Infrastructure.Repositories.Interfaces;

public interface ITransactionRepository
{
    Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId, CancellationToken cancellationToken = default);
    Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId, CancellationToken cancellationToken = default);
    Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber, CancellationToken cancellationToken = default);
    Task<PendingFinalizeInfo?> GetPendingFinalizeInfoAsync(int pendingTrxId, CancellationToken cancellationToken = default);
    Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType, CancellationToken cancellationToken = default);
    Task UpdatePendingTrxApprovalNumberAsync(int pendingTrxId, string approvalNumber, CancellationToken cancellationToken = default);

    Task<List<TransactionSearchResult>> SearchTransactionsAsync(int merchantId, TransactionStatusType status, int? transactionId = null, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    Task<List<TransactionSearchResult>> SearchCapturedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
    Task<List<TransactionSearchResult>> SearchDeclinedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
    Task<List<TransactionSearchResult>> SearchAuthorizedTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
    Task<List<TransactionSearchResult>> SearchPendingTransactionsAsync(int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default);
}