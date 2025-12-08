using Microsoft.EntityFrameworkCore;
using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly EzzygateDbContext _context;

    public TransactionRepository(EzzygateDbContext context)
    {
        _context = context;
    }

    public async Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransApprovals
            .FirstOrDefaultAsync(e => e.Id == approvalTrxId, cancellationToken: cancellationToken);

        return entity?.ToDomain();
    }
    public async Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.Id == pendingTrxId, cancellationToken: cancellationToken);

        return entity?.ToDomain();
    }

    public async Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.DebitApprovalNumber == approvalNumber, cancellationToken: cancellationToken);

        return entity?.ToDomain();
    }

    public async Task<PendingFinalizeInfo?> GetPendingFinalizeInfoAsync(int pendingTrxId, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblLogPendingFinalizes
            .FirstOrDefaultAsync(f => f.PendingId == pendingTrxId, cancellationToken: cancellationToken);

        return entity?.ToDomain();
    }

    public async Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransApprovals
            .FirstOrDefaultAsync(e => e.Id == approvalTrxId, cancellationToken: cancellationToken);
        if (entity == null)
            throw new Exception($"Approval trx not found '{approvalTrxId}'");

        entity.AuthStatus = (byte)opType;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePendingTrxApprovalNumberAsync(int pendingTrxId, string approvalNumber, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.Id == pendingTrxId, cancellationToken: cancellationToken);
        if (entity == null)
            throw new Exception($"Pending trx not found '{pendingTrxId}'");

        entity.DebitApprovalNumber = approvalNumber;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateApprovalTrxRecurringAsync(int transactionId, int recurringSeriesId, int chargeNumber = 1, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransApprovals
            .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);

        if (entity != null)
        {
            entity.RecurringSeries = recurringSeriesId;
            entity.RecurringChargeNumber = chargeNumber;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdatePassTrxRecurringAsync(int transactionId, int recurringSeriesId, int chargeNumber = 1, CancellationToken cancellationToken = default)
    {
        var entity = await _context.TblCompanyTransPasses
            .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);

        if (entity != null)
        {
            entity.RecurringSeries = recurringSeriesId;
            entity.RecurringChargeNumber = chargeNumber;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<List<TransactionSearchResult>> SearchTransactionsAsync(
        int merchantId, TransactionStatusType status, int? transactionId = null, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
    {
        return status switch
        {
            TransactionStatusType.Captured => await SearchCapturedTransactionsAsync(merchantId, transactionId, from, to, cancellationToken),
            TransactionStatusType.Declined => await SearchDeclinedTransactionsAsync(merchantId, transactionId, from, to, cancellationToken),
            TransactionStatusType.Authorized => await SearchAuthorizedTransactionsAsync(merchantId, transactionId, from, to, cancellationToken),
            TransactionStatusType.Pending => await SearchPendingTransactionsAsync(merchantId, transactionId, from, to, cancellationToken),
            _ => []
        };
    }

    public async Task<List<TransactionSearchResult>> SearchCapturedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var query = _context.TblCompanyTransPasses
            .Include(t => t.TransPayerInfo)
            .Include(t => t.CurrencyNavigation)
            .Where(t => t.CompanyId == merchantId);

        if (transactionId.HasValue)
            query = query.Where(t => t.Id == transactionId.Value);
        if (from.HasValue)
            query = query.Where(t => t.InsertDate >= from.Value);
        if (to.HasValue)
            query = query.Where(t => t.InsertDate <= to.Value);

        var results = await query.ToListAsync(cancellationToken);
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchDeclinedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var query = _context.TblCompanyTransFails
            .Include(t => t.TransPayerInfo)
            .Include(t => t.CurrencyNavigation)
            .Where(t => t.CompanyId == merchantId);

        if (transactionId.HasValue)
            query = query.Where(t => t.Id == transactionId.Value);
        if (from.HasValue)
            query = query.Where(t => t.InsertDate >= from.Value);
        if (to.HasValue)
            query = query.Where(t => t.InsertDate <= to.Value);

        var results = await query.ToListAsync(cancellationToken);
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchAuthorizedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var query = _context.TblCompanyTransApprovals
            .Include(t => t.TransPayerInfo)
            .Include(t => t.CurrencyNavigation)
            .Where(t => t.CompanyId == merchantId);

        if (transactionId.HasValue)
            query = query.Where(t => t.Id == transactionId.Value);
        if (from.HasValue)
            query = query.Where(t => t.InsertDate >= from.Value);
        if (to.HasValue)
            query = query.Where(t => t.InsertDate <= to.Value);

        var results = await query.ToListAsync(cancellationToken);
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchPendingTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to, CancellationToken cancellationToken = default)
    {
        var query = _context.TblCompanyTransPendings
            .Include(t => t.TransPayerInfo)
            .Include(t => t.TransCurrencyNavigation)
            .Where(t => t.CompanyId == merchantId);

        if (transactionId.HasValue)
            query = query.Where(t => t.Id == transactionId.Value);
        if (from.HasValue)
            query = query.Where(t => t.InsertDate >= from.Value);
        if (to.HasValue)
            query = query.Where(t => t.InsertDate <= to.Value);

        var results = await query.ToListAsync(cancellationToken);
        return results.Select(x => x.ToSearchResult()).ToList();
    }
}