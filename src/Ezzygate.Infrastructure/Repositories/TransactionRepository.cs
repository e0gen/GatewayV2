using Microsoft.Extensions.Logging;
using Ezzygate.Application.Models;
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

    public async Task UpdateChargeAttemptAsync(int pendingTrxId, int movedTrxId, string replyCode,
        string errorMessage)
    {
        var chargeLog = _context.TblLogChargeAttempts
            .SingleOrDefault(e => e.LcaTransNum == pendingTrxId && e.LcaReplyCode == "553");
        if (chargeLog == null) return;
        chargeLog.LcaTransNum = movedTrxId;
        chargeLog.LcaReplyCode = replyCode;
        chargeLog.LcaReplyDesc = errorMessage;
        await _context.SaveChangesAsync();
    }

    public async Task<MoveTransactionResult> MoveTrxAsync(
        int pendingId, string replyCode, string message, string? binCountryIso)
    {
        var pendingTrx = _context.TblCompanyTransPendings
            .SingleOrDefault(t => t.Id == pendingId);
        if (pendingTrx == null)
            throw new Exception($"Pending trx '{pendingId}' not found");

        var finalizeRow = _context.TblLogPendingFinalizes
            .SingleOrDefault(e => e.PendingId == pendingId);
        var isFinalized = finalizeRow != null;
        finalizeRow ??= new TblLogPendingFinalize { PendingId = pendingId, FinalizeDate = DateTime.Now };

        var cart = _context.Carts.SingleOrDefault(e => e.TransPendingId == pendingId);

        replyCode = replyCode.Trim();
        int? trxId;
        switch (replyCode)
        {
            case "000" when pendingTrx.TransType == 0 || pendingTrx.TransType == 3:
            {
                trxId = await InsertPassedTrxAsync(pendingTrx, replyCode, binCountryIso);
                finalizeRow.TransPassId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    cart.TransPassId = trxId;
                    await _context.SaveChangesAsync();
                }

                break;
            }
            case "000" when pendingTrx.TransType == 1:
            {
                trxId = await InsertApprovedTrxAsync(pendingTrx, replyCode, binCountryIso);
                finalizeRow.TransApprovalId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    cart.TransPreAuthId = trxId;
                    await _context.SaveChangesAsync();
                }

                break;
            }
            case "000":
                throw new Exception($"Invalid trans type '{pendingTrx.TransType}'");
            case "553":
                trxId = pendingId;
                break;
            default:
            {
                trxId = await InsertFailTrxAsync(pendingTrx, replyCode, message, binCountryIso);
                finalizeRow.TransFailId = trxId;

                if (cart != null)
                {
                    cart.TransPendingId = null;
                    await _context.SaveChangesAsync();
                }

                break;
            }
        }

        if (replyCode != "553")
        {
            if (!isFinalized)
            {
                _context.TblLogPendingFinalizes.Add(finalizeRow);
                await _context.SaveChangesAsync();
            }

            try
            {
                _context.TblCompanyTransPendings.Add(pendingTrx);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(LogTag.WebApi, ex, "Can't remove pending trx {pendingTrx.Id}", pendingTrx.Id);
            }
        }

        return new MoveTransactionResult(trxId.Value, pendingTrx.ToDomain());
    }

    private async Task<int> InsertPassedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode,
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

    private async Task<int> InsertApprovedTrxAsync(TblCompanyTransPending pendingTrx, string replyCode,
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
            //PayId = ";0;",
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

    private async Task<int> InsertFailTrxAsync(TblCompanyTransPending pendingTrx, string replyCode, string message,
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
            //PayId = ";0;",
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
}