using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ezzygate.Application.Transactions;
using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Ef.Entities;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Infrastructure.Mappings;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly EzzygateDbContext _context;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ILogger<TransactionRepository> _logger;

    public TransactionRepository(EzzygateDbContext context, ICurrencyRepository currencyRepository,
        ILogger<TransactionRepository> logger)
    {
        _context = context;
        _currencyRepository = currencyRepository;
        _logger = logger;
    }

    public async Task<ApprovalTransaction?> GetApprovalTrxAsync(int approvalTrxId)
    {
        var entity = await _context.TblCompanyTransApprovals
            .FirstOrDefaultAsync(e => e.Id == approvalTrxId);

        return entity?.ToDomain();
    }
    public async Task<PendingTransaction?> GetPendingTrxByIdAsync(int pendingTrxId)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.Id == pendingTrxId);

        return entity?.ToDomain();
    }

    public async Task<PendingTransaction?> GetPendingTrxByApprovalNumberAsync(string approvalNumber)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.DebitApprovalNumber == approvalNumber);

        return entity?.ToDomain();
    }

    public async Task<PendingFinalizeInfo?> GetPendingFinalizeInfoAsync(int pendingTrxId)
    {
        var entity = await _context.TblLogPendingFinalizes
            .FirstOrDefaultAsync(f => f.PendingId == pendingTrxId);

        return entity?.ToDomain();
    }

    public async Task<TblCompanyTransPending?> GetPendingTrxEntityAsync(int pendingTrxId)
    {
        return await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(t => t.Id == pendingTrxId);
    }

    public async Task UpdateApprovalTrxAuthStatusAsync(int approvalTrxId, OperationType opType)
    {
        var entity = await _context.TblCompanyTransApprovals
            .FirstOrDefaultAsync(e => e.Id == approvalTrxId);
        if (entity == null)
            throw new Exception($"Approval trx not found '{approvalTrxId}'");

        entity.AuthStatus = (byte)opType;
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePendingTrxApprovalNumberAsync(int pendingTrxId, string approvalNumber)
    {
        var entity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(e => e.Id == pendingTrxId);
        if (entity == null)
            throw new Exception($"Pending trx not found '{pendingTrxId}'");

        entity.DebitApprovalNumber = approvalNumber;
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCartForFinalizedTrxAsync(int pendingId, int? passId, int? approvalId)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(e => e.TransPendingId == pendingId);
        if (cart == null)
            return;

        cart.TransPendingId = null;
        cart.TransPassId = passId;
        cart.TransPreAuthId = approvalId;
        await _context.SaveChangesAsync();
    }

    public async Task AddPendingFinalizeAsync(PendingFinalizeInfo finalizeInfo)
    {
        var entity = finalizeInfo.ToEntity();
        _context.TblLogPendingFinalizes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<int> AddPassedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode,
        string? binCountryIso)
    {
        var currency = _currencyRepository.Get(pendingTrx.Currency.Value);
        var mockDate = DateTime.Parse("1900/01/01 00:00:00");

        var trx = new TblCompanyTransPass
        {
            CompanyId = pendingTrx.CompanyId.Value,
            DebitCompanyId = pendingTrx.DebitCompanyId,
            CreditType = pendingTrx.TransCreditType,
            PaymentMethod = pendingTrx.PaymentMethod,
            InsertDate = DateTime.Now,
            DeniedDate = mockDate,
            DeniedPrintDate = mockDate,
            DeniedSendDate = mockDate,
            MerchantPd = mockDate,
            Pd = mockDate,
            Payments = pendingTrx.TransPayments,
            Currency = currency.Id,
            Ocurrency = (byte)currency.Id,
            Amount = pendingTrx.TransAmount,
            Oamount = pendingTrx.TransAmount,
            TransSourceId = pendingTrx.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            PayforText = pendingTrx.PayforText,
            CustomerId = pendingTrx.CustomerId,
            PaymentMethodDisplay = pendingTrx.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingTrx.Ipaddress.EmptyIfNull(),
            TerminalNumber = pendingTrx.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingTrx.DebitReferenceCode,
            DebitReferenceNum = pendingTrx.DebitReferenceNum,
            PayId = ";0;",
            OrderNumber = pendingTrx.TransOrder.EmptyIfNull(),
            ApprovalNumber = pendingTrx.DebitApprovalNumber.EmptyIfNull(),
            DeniedAdminComment = "",
            ReferringUrl = "",
            PayerIdUsed = "",
            Ipcountry = "",
            UnsettledAmount = pendingTrx.TransAmount,
            UnsettledInstallments = pendingTrx.TransPayments,
            TransPayerInfoId = pendingTrx.TransPayerInfoId,
            CreditCardId = pendingTrx.CreditCardId,
            PaymentMethodId = (byte)pendingTrx.CreditCardId.Value,
            TransPaymentMethodId = pendingTrx.TransPaymentMethodId,
            IsTestOnly = pendingTrx.IsTestOnly,
            Carts = pendingTrx.Carts,
            Comment = pendingTrx.Comment
        };

        // fees
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingTrx.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingTrx.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        if (merchantFees != null)
        {
            trx.EzzygateFeeTransactionCharge = merchantFees.CcfFixedFee;
            trx.EzzygateFeeRatioCharge = (pendingTrx.TransAmount * merchantFees.CcfPercentFee) / 100;
        }
        else
        {
            trx.EzzygateFeeTransactionCharge = 0;
            trx.EzzygateFeeRatioCharge = 0;
        }

        // debit company fees
        var debitFees = _context.TblDebitCompanyFees
            .FirstOrDefault(f => f.DcfDebitCompanyId == pendingTrx.DebitCompanyId
                                 && f.DcfCurrencyId == pendingTrx.Currency
                                 && f.DcfPaymentMethod == pendingTrx.PaymentMethod
                                 && f.DcfTerminalNumber.Trim() == pendingTrx.TerminalNumber.Trim());
        trx.DebitFee = debitFees?.DcfFixedFee ?? 0;

        _context.TblCompanyTransPasses.Add(trx);
        await _context.SaveChangesAsync();

        return trx.Id;
    }

    public async Task<int> AddApprovedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode,
        string? binCountryIso)
    {
        var currency = _currencyRepository.Get(pendingTrx.Currency.Value);

        var trx = new TblCompanyTransApproval
        {
            CompanyId = pendingTrx.CompanyId.Value,
            DebitCompanyId = pendingTrx.DebitCompanyId,
            CreditType = pendingTrx.TransCreditType,
            PaymentMethod = pendingTrx.PaymentMethod,
            InsertDate = DateTime.Now,
            Payments = pendingTrx.TransPayments,
            Currency = currency.Id,
            Amount = pendingTrx.TransAmount,
            TransSourceId = pendingTrx.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            PayforText = pendingTrx.PayforText,
            CustomerId = pendingTrx.CustomerId,
            PaymentMethodDisplay = pendingTrx.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingTrx.Ipaddress.EmptyIfNull(),
            TerminalNumber = pendingTrx.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingTrx.DebitReferenceCode,
            DebitReferenceNum = pendingTrx.DebitReferenceNum,
            OrderNumber = pendingTrx.TransOrder.EmptyIfNull(),
            ApprovalNumber = pendingTrx.DebitApprovalNumber.EmptyIfNull(),
            ReferringUrl = "",
            TransPayerInfoId = pendingTrx.TransPayerInfoId,
            CreditCardId = pendingTrx.CreditCardId,
            PaymentMethodId = (byte)pendingTrx.CreditCardId.Value,
            TransPaymentMethodId = pendingTrx.TransPaymentMethodId,
            IsTestOnly = pendingTrx.IsTestOnly,
            Carts = pendingTrx.Carts,
            Comment = pendingTrx.Comment
        };

        // fees            
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingTrx.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingTrx.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        trx.EzzygateFeeTransactionCharge = merchantFees?.CcfCbfixedFee ?? 0;

        // debit company fees
        var debitFees = _context.TblDebitCompanyFees
            .FirstOrDefault(f => f.DcfDebitCompanyId == pendingTrx.DebitCompanyId
                                 && f.DcfCurrencyId == pendingTrx.Currency
                                 && f.DcfPaymentMethod == pendingTrx.PaymentMethod
                                 && f.DcfTerminalNumber.Trim() == pendingTrx.TerminalNumber.Trim());
        trx.DebitFee = debitFees?.DcfFixedFee ?? 0;

        _context.TblCompanyTransApprovals.Add(trx);
        await _context.SaveChangesAsync();

        return trx.Id;
    }

    public async Task<int> AddFailTrxAsync(TblCompanyTransPending pendingTrx, string replyCode, string message,
        string? binCountryIso)
    {
        var currency = _currencyRepository.Get(pendingTrx.Currency.Value);

        var trx = new TblCompanyTransFail
        {
            CompanyId = pendingTrx.CompanyId.Value,
            DebitCompanyId = pendingTrx.DebitCompanyId,
            CreditType = pendingTrx.TransCreditType,
            PaymentMethod = pendingTrx.PaymentMethod,
            InsertDate = DateTime.Now,
            Payments = pendingTrx.TransPayments,
            Currency = currency.Id,
            Amount = pendingTrx.TransAmount,
            TransSourceId = pendingTrx.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            DebitDeclineReason = message,
            PayforText = pendingTrx.PayforText,
            CustomerId = pendingTrx.CustomerId,
            PaymentMethodDisplay = pendingTrx.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingTrx.Ipaddress.EmptyIfNull(),
            TerminalNumber = pendingTrx.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingTrx.DebitReferenceCode,
            DebitReferenceNum = pendingTrx.DebitReferenceNum,
            OrderNumber = pendingTrx.TransOrder.EmptyIfNull(),
            ReferringUrl = "",
            PayerIdUsed = "",
            Ipcountry = "",
            TransPayerInfoId = pendingTrx.TransPayerInfoId,
            CreditCardId = pendingTrx.CreditCardId,
            PaymentMethodId = (byte)pendingTrx.CreditCardId.Value,
            TransPaymentMethodId = pendingTrx.TransPaymentMethodId,
            IsTestOnly = pendingTrx.IsTestOnly,
            Comment = pendingTrx.Comment
        };

        // fees
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingTrx.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingTrx.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        trx.EzzygateFeeTransactionCharge = merchantFees?.CcfFixedFee ?? 0;

        _context.TblCompanyTransFails.Add(trx);
        await _context.SaveChangesAsync();

        return trx.Id;
    }

    public async Task RemovePendingTrxAsync(TblCompanyTransPending pendingTrx)
    {
        try
        {
            _context.TblCompanyTransPendings.Remove(pendingTrx);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.Error(LogTag.WebApi, ex, "Can't remove pending trx {PendingTrxId}", pendingTrx.Id);
        }
    }

    public async Task<List<TransactionSearchResult>> SearchTransactionsAsync(
        int merchantId, TransactionStatusType status, int? transactionId = null, DateTime? from = null, DateTime? to = null)
    {
        return status switch
        {
            TransactionStatusType.Captured => await SearchCapturedTransactionsAsync(merchantId, transactionId, from, to),
            TransactionStatusType.Declined => await SearchDeclinedTransactionsAsync(merchantId, transactionId, from, to),
            TransactionStatusType.Authorized => await SearchAuthorizedTransactionsAsync(merchantId, transactionId, from, to),
            TransactionStatusType.Pending => await SearchPendingTransactionsAsync(merchantId, transactionId, from, to),
            _ => []
        };
    }

    public async Task<List<TransactionSearchResult>> SearchCapturedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to)
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

        var results = await query.ToListAsync();
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchDeclinedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to)
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

        var results = await query.ToListAsync();
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchAuthorizedTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to)
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

        var results = await query.ToListAsync();
        return results.Select(x => x.ToSearchResult()).ToList();
    }

    public async Task<List<TransactionSearchResult>> SearchPendingTransactionsAsync(
        int merchantId, int? transactionId, DateTime? from, DateTime? to)
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

        var results = await query.ToListAsync();
        return results.Select(x => x.ToSearchResult()).ToList();
    }
}