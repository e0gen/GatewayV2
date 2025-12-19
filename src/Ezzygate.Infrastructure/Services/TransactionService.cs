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

namespace Ezzygate.Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly EzzygateDbContext _context;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(
        EzzygateDbContext context,
        ICurrencyRepository currencyRepository,
        ITransactionRepository transactionRepository,
        ILogger<TransactionService> logger)
    {
        _context = context;
        _currencyRepository = currencyRepository;
        _transactionRepository = transactionRepository;
        _logger = logger;
    }

    public async Task<PendingLookupResult?> LocatePendingAsync(int pendingTransactionId, int merchantId, CancellationToken cancellationToken = default)
    {
        var finalizeInfo = await _transactionRepository.GetPendingFinalizeInfoAsync(pendingTransactionId, cancellationToken);
        if (finalizeInfo != null)
        {
            if (finalizeInfo.TransPassId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Captured, finalizeInfo.TransPassId.Value, cancellationToken: cancellationToken);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Captured, finalizeInfo.TransPassId.Value);
            }

            if (finalizeInfo.TransFailId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Declined, finalizeInfo.TransFailId.Value, cancellationToken: cancellationToken);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Declined, finalizeInfo.TransFailId.Value);
            }

            if (finalizeInfo.TransApprovalId.HasValue)
            {
                var results = await _transactionRepository.SearchTransactionsAsync(
                    merchantId, TransactionStatusType.Authorized, finalizeInfo.TransApprovalId.Value, cancellationToken: cancellationToken);
                if (results.Count != 0)
                    return new PendingLookupResult(TransactionStatusType.Authorized, finalizeInfo.TransApprovalId.Value);
            }
        }

        var pendingResults = await _transactionRepository.SearchTransactionsAsync(
            merchantId, TransactionStatusType.Pending, pendingTransactionId, cancellationToken: cancellationToken);
        return pendingResults.Count != 0 ? new PendingLookupResult(TransactionStatusType.Pending, pendingTransactionId) : null;
    }

    public async Task<MoveTransactionResult> MoveTrxAsync(int pendingId, string replyCode, string? message, string? binCountryIso, CancellationToken cancellationToken = default)
    {
        var pendingEntity = await _context.TblCompanyTransPendings
            .FirstOrDefaultAsync(p => p.Id == pendingId, cancellationToken);

        if (pendingEntity == null)
            throw new Exception($"Pending trx '{pendingId}' not found");

        var finalizeRow = await _context.TblLogPendingFinalizes
            .FirstOrDefaultAsync(f => f.PendingId == pendingId, cancellationToken);
        var isFinalized = finalizeRow != null;
        if (!isFinalized)
        {
            finalizeRow = new TblLogPendingFinalize
            {
                PendingId = pendingId,
                FinalizeDate = DateTime.Now
            };
        }

        var cart = await _context.Carts
            .FirstOrDefaultAsync(c => c.TransPendingId == pendingId, cancellationToken);

        replyCode = replyCode.Trim();
        int? trxId;

        switch (replyCode)
        {
            case "000" when pendingEntity.TransType == 0 || pendingEntity.TransType == 3:
            {
                trxId = await InsertPassedTrxAsync(pendingEntity, replyCode, binCountryIso, cancellationToken);
                finalizeRow!.TransPassId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    cart.TransPassId = trxId;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                break;
            }
            case "000" when pendingEntity.TransType == 1:
            {
                trxId = await InsertApprovedTrxAsync(pendingEntity, replyCode, binCountryIso, cancellationToken);
                finalizeRow!.TransApprovalId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    cart.TransPreAuthId = trxId;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                break;
            }
            case "000":
                throw new Exception($"Invalid trans type '{pendingEntity.TransType}'");
            case "553":
                trxId = pendingId;
                break;
            default:
            {
                trxId = await InsertFailTrxAsync(pendingEntity, replyCode, message, binCountryIso, cancellationToken);
                finalizeRow!.TransFailId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                break;
            }
        }

        if (replyCode != "553")
        {
            if (!isFinalized)
            {
                _context.TblLogPendingFinalizes.Add(finalizeRow!);
                await _context.SaveChangesAsync(cancellationToken);
            }

            try
            {
                _context.TblCompanyTransPendings.Remove(pendingEntity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.Error(LogTag.WebApiV2, ex, "Can't remove pending trx {PendingTrxId}", pendingEntity.Id);
            }
        }

        var pendingTrx = pendingEntity.ToDomain();
        return new MoveTransactionResult(trxId.Value, pendingTrx);
    }

    private async Task<int> InsertPassedTrxAsync(TblCompanyTransPending pendingEntity, string replyCode, string? binCountryIso, CancellationToken cancellationToken)
    {
        var currency = _currencyRepository.Get(pendingEntity.Currency!.Value);
        var mockDate = DateTime.Parse("1900/01/01 00:00:00");

        var trx = new TblCompanyTransPass
        {
            CompanyId = pendingEntity.CompanyId!.Value,
            DebitCompanyId = pendingEntity.DebitCompanyId,
            CreditType = pendingEntity.TransCreditType,
            PaymentMethod = pendingEntity.PaymentMethod,
            InsertDate = DateTime.Now,
            DeniedDate = mockDate,
            DeniedPrintDate = mockDate,
            DeniedSendDate = mockDate,
            MerchantPd = mockDate,
            Pd = mockDate,
            Payments = pendingEntity.TransPayments,
            Currency = currency.Id,
            Ocurrency = (byte)currency.Id,
            Amount = pendingEntity.TransAmount,
            Oamount = pendingEntity.TransAmount,
            TransSourceId = pendingEntity.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            PayforText = pendingEntity.PayforText,
            CustomerId = pendingEntity.CustomerId,
            PaymentMethodDisplay = pendingEntity.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingEntity.IpAddress.EmptyIfNull(),
            TerminalNumber = pendingEntity.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingEntity.DebitReferenceCode,
            DebitReferenceNum = pendingEntity.DebitReferenceNum,
            PayId = ";0;",
            OrderNumber = pendingEntity.TransOrder.EmptyIfNull(),
            ApprovalNumber = pendingEntity.DebitApprovalNumber.EmptyIfNull(),
            DeniedAdminComment = "",
            ReferringUrl = "",
            PayerIdUsed = "",
            Ipcountry = "",
            UnsettledAmount = pendingEntity.TransAmount,
            UnsettledInstallments = pendingEntity.TransPayments,
            TransPayerInfoId = pendingEntity.TransPayerInfoId,
            CreditCardId = pendingEntity.CreditCardId,
            PaymentMethodId = (byte)pendingEntity.CreditCardId!.Value,
            TransPaymentMethodId = pendingEntity.TransPaymentMethodId,
            IsTestOnly = pendingEntity.IsTestOnly,
            Carts = pendingEntity.Carts,
            Comment = pendingEntity.Comment
        };

        // Calculate fees
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingEntity.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingEntity.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        if (merchantFees != null)
        {
            trx.EzzygateFeeTransactionCharge = merchantFees.CcfFixedFee;
            trx.EzzygateFeeRatioCharge = (pendingEntity.TransAmount * merchantFees.CcfPercentFee) / 100;
        }
        else
        {
            trx.EzzygateFeeTransactionCharge = 0;
            trx.EzzygateFeeRatioCharge = 0;
        }

        // Debit company fees
        var debitFees = _context.TblDebitCompanyFees
            .FirstOrDefault(f => f.DcfDebitCompanyId == pendingEntity.DebitCompanyId
                                 && f.DcfCurrencyId == pendingEntity.Currency
                                 && f.DcfPaymentMethod == pendingEntity.PaymentMethod
                                 && f.DcfTerminalNumber.Trim() == pendingEntity.TerminalNumber.Trim());
        trx.DebitFee = debitFees?.DcfFixedFee ?? 0;

        _context.TblCompanyTransPasses.Add(trx);
        await _context.SaveChangesAsync(cancellationToken);

        return trx.Id;
    }

    private async Task<int> InsertApprovedTrxAsync(TblCompanyTransPending pendingEntity, string replyCode, string? binCountryIso, CancellationToken cancellationToken)
    {
        var currency = _currencyRepository.Get(pendingEntity.Currency!.Value);

        var trx = new TblCompanyTransApproval
        {
            CompanyId = pendingEntity.CompanyId!.Value,
            DebitCompanyId = pendingEntity.DebitCompanyId,
            CreditType = pendingEntity.TransCreditType,
            PaymentMethod = pendingEntity.PaymentMethod,
            InsertDate = DateTime.Now,
            Payments = pendingEntity.TransPayments,
            Currency = currency.Id,
            Amount = pendingEntity.TransAmount,
            TransSourceId = pendingEntity.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            PayforText = pendingEntity.PayforText,
            CustomerId = pendingEntity.CustomerId,
            PaymentMethodDisplay = pendingEntity.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingEntity.IpAddress.EmptyIfNull(),
            TerminalNumber = pendingEntity.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingEntity.DebitReferenceCode,
            DebitReferenceNum = pendingEntity.DebitReferenceNum,
            OrderNumber = pendingEntity.TransOrder.EmptyIfNull(),
            ApprovalNumber = pendingEntity.DebitApprovalNumber.EmptyIfNull(),
            ReferringUrl = "",
            TransPayerInfoId = pendingEntity.TransPayerInfoId,
            CreditCardId = pendingEntity.CreditCardId,
            PaymentMethodId = (byte)pendingEntity.CreditCardId!.Value,
            TransPaymentMethodId = pendingEntity.TransPaymentMethodId,
            IsTestOnly = pendingEntity.IsTestOnly,
            Carts = pendingEntity.Carts,
            Comment = pendingEntity.Comment
        };

        // Calculate fees
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingEntity.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingEntity.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        trx.EzzygateFeeTransactionCharge = merchantFees?.CcfCbfixedFee ?? 0;

        // Debit company fees
        var debitFees = _context.TblDebitCompanyFees
            .FirstOrDefault(f => f.DcfDebitCompanyId == pendingEntity.DebitCompanyId
                                 && f.DcfCurrencyId == pendingEntity.Currency
                                 && f.DcfPaymentMethod == pendingEntity.PaymentMethod
                                 && f.DcfTerminalNumber.Trim() == pendingEntity.TerminalNumber.Trim());
        trx.DebitFee = debitFees?.DcfFixedFee ?? 0;

        _context.TblCompanyTransApprovals.Add(trx);
        await _context.SaveChangesAsync(cancellationToken);

        return trx.Id;
    }

    private async Task<int> InsertFailTrxAsync(TblCompanyTransPending pendingEntity, string replyCode, string? message, string? binCountryIso, CancellationToken cancellationToken)
    {
        var currency = _currencyRepository.Get(pendingEntity.Currency!.Value);

        var trx = new TblCompanyTransFail
        {
            CompanyId = pendingEntity.CompanyId!.Value,
            DebitCompanyId = pendingEntity.DebitCompanyId,
            CreditType = pendingEntity.TransCreditType,
            PaymentMethod = pendingEntity.PaymentMethod,
            InsertDate = DateTime.Now,
            Payments = pendingEntity.TransPayments,
            Currency = currency.Id,
            Amount = pendingEntity.TransAmount,
            TransSourceId = pendingEntity.TransSourceId,
            ReplyCode = replyCode.EmptyIfNull(),
            DebitDeclineReason = message,
            PayforText = pendingEntity.PayforText,
            CustomerId = pendingEntity.CustomerId,
            PaymentMethodDisplay = pendingEntity.PaymentMethodDisplay.EmptyIfNull(),
            Ipaddress = pendingEntity.IpAddress.EmptyIfNull(),
            TerminalNumber = pendingEntity.TerminalNumber.EmptyIfNull(),
            DebitReferenceCode = pendingEntity.DebitReferenceCode,
            DebitReferenceNum = pendingEntity.DebitReferenceNum,
            OrderNumber = pendingEntity.TransOrder.EmptyIfNull(),
            ReferringUrl = "",
            PayerIdUsed = "",
            Ipcountry = "",
            TransPayerInfoId = pendingEntity.TransPayerInfoId,
            CreditCardId = pendingEntity.CreditCardId,
            PaymentMethodId = (byte)pendingEntity.CreditCardId!.Value,
            TransPaymentMethodId = pendingEntity.TransPaymentMethodId,
            IsTestOnly = pendingEntity.IsTestOnly,
            Comment = pendingEntity.Comment
        };

        // Calculate fees
        binCountryIso ??= string.Empty;
        var fees = _context.TblCompanyCreditFees
            .Where(f => f.CcfPaymentMethod == pendingEntity.PaymentMethod
                        && f.CcfCurrencyId == currency.Id
                        && f.CcfCompanyId == pendingEntity.CompanyId
                        && f.CcfIsDisabled == false
                        && (f.CcfListBins == "" || f.CcfListBins.Contains(binCountryIso)))
            .ToList();
        var merchantFees =
            fees.FirstOrDefault(f => f.CcfListBins.Contains(binCountryIso, StringComparison.CurrentCultureIgnoreCase))
            ?? fees.FirstOrDefault(f => f.CcfListBins == "");
        trx.EzzygateFeeTransactionCharge = merchantFees?.CcfFixedFee ?? 0;

        _context.TblCompanyTransFails.Add(trx);
        await _context.SaveChangesAsync(cancellationToken);

        return trx.Id;
    }
}