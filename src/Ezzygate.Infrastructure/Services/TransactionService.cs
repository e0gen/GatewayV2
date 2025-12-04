using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string message, string? binCountryIso)
    {
        var pendingTrx = await _transactionRepository.GetPendingTrxEntityAsync(pendingId);
        if (pendingTrx == null)
            throw new Exception($"Pending trx '{pendingId}' not found");

        var existingFinalize = await _transactionRepository.GetPendingFinalizeInfoAsync(pendingId);
        var isFinalized = existingFinalize != null;

        var finalizeInfo = new PendingFinalizeInfo(
            PendingId: pendingId,
            FinalizeDate: DateTime.Now,
            TransPassId: null,
            TransFailId: null,
            TransApprovalId: null);

        replyCode = replyCode.Trim();
        int? trxId;

        switch (replyCode)
        {
            case "000" when pendingTrx.TransType == 0 || pendingTrx.TransType == 3:
            {
                trxId = await _transactionRepository.AddPassedTrxAsync(pendingTrx, replyCode, binCountryIso);
                finalizeInfo = finalizeInfo with { TransPassId = trxId };
                await _transactionRepository.UpdateCartForFinalizedTrxAsync(pendingId, trxId, null);
                break;
            }
            case "000" when pendingTrx.TransType == 1:
            {
                trxId = await _transactionRepository.AddApprovedTrxAsync(pendingTrx, replyCode, binCountryIso);
                finalizeInfo = finalizeInfo with { TransApprovalId = trxId };
                await _transactionRepository.UpdateCartForFinalizedTrxAsync(pendingId, null, trxId);
                break;
            }
            case "000":
                throw new Exception($"Invalid trans type '{pendingTrx.TransType}'");
            case "553":
                trxId = pendingId;
                break;
            default:
            {
                trxId = await _transactionRepository.AddFailTrxAsync(pendingTrx, replyCode, message, binCountryIso);
                finalizeInfo = finalizeInfo with { TransFailId = trxId };
                await _transactionRepository.UpdateCartForFinalizedTrxAsync(pendingId, null, null);
                break;
            }
        }

        if (replyCode != "553")
        {
            if (!isFinalized)
                await _transactionRepository.AddPendingFinalizeAsync(finalizeInfo);

            await _transactionRepository.RemovePendingTrxAsync(pendingTrx);
        }

        return new MoveTransactionResult(trxId.Value, pendingTrx.ToDomain());
    }

    public async Task<PendingLookupResult?> LocatePendingAsync(int pendingTransactionId, int merchantId)
    {
        var finalizeInfo = await _transactionRepository.GetPendingFinalizeInfoAsync(pendingTransactionId);
        if (finalizeInfo != null)
        {
            if (finalizeInfo.TransPassId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Captured, finalizeInfo.TransPassId.Value);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Captured, finalizeInfo.TransPassId.Value);
            }

            if (finalizeInfo.TransFailId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Declined, finalizeInfo.TransFailId.Value);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Declined, finalizeInfo.TransFailId.Value);
            }

            if (finalizeInfo.TransApprovalId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Authorized, finalizeInfo.TransApprovalId.Value);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Authorized, finalizeInfo.TransApprovalId.Value);
            }
        }

        var pendingResults = await _transactionRepository.SearchTransactionsAsync(
            merchantId, TransactionStatusType.Pending, pendingTransactionId);
        return pendingResults.Count != 0 ? new PendingLookupResult(TransactionStatusType.Pending, pendingTransactionId) : null;
    }
}