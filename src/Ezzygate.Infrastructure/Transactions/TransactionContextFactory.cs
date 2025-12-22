using Microsoft.EntityFrameworkCore;
using Ezzygate.Application.Configuration;
using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Repositories.Interfaces;

namespace Ezzygate.Infrastructure.Transactions;

public class TransactionContextFactory : ITransactionContextFactory
{
    private readonly EzzygateDbContext _context;
    private readonly IDomainConfiguration _domainConfiguration;
    private readonly ITerminalRepository _terminalRepository;
    private readonly IChargeAttemptRepository _chargeAttemptRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public TransactionContextFactory(
        EzzygateDbContext context,
        IDomainConfiguration domainConfiguration,
        ITerminalRepository terminalRepository,
        IChargeAttemptRepository chargeAttemptRepository,
        IPaymentMethodRepository paymentMethodRepository)
    {
        _context = context;
        _domainConfiguration = domainConfiguration;
        _terminalRepository = terminalRepository;
        _chargeAttemptRepository = chargeAttemptRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<TransactionContext> CreateAsync(string referenceCode, int logChargeAttemptId)
    {
        var locatedTrx = await CreateTransactionContextFromReferenceAsync(referenceCode);
        if (locatedTrx == null)
            throw new Exception($"Trx with reference code {referenceCode} not found");

        var attemptLog = await _chargeAttemptRepository.GetByIdAsync(logChargeAttemptId);
        if (attemptLog != null)
            locatedTrx.OpType = GetOperationType(attemptLog, locatedTrx);

        var terminal = await _terminalRepository.GetByTerminalNumberAsync(locatedTrx.TerminalNumber);
        if (terminal == null)
            throw new Exception($"Terminal with number {locatedTrx.TerminalNumber} not found");

        var debitCompany = await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId);
        if (debitCompany == null)
            throw new Exception($"Bank with id {terminal.DebitCompanyId} not found");

        var context = new TransactionContext(_domainConfiguration)
        {
            LocatedTrx = locatedTrx,
            Terminal = terminal,
            DebitCompany = debitCompany,
        };

        return context;
    }

    public async Task<TransactionContext> CreateAsync(int terminalId)
    {
        var terminal = await _terminalRepository.GetByIdAsync(terminalId);
        if (terminal == null)
            throw new Exception($"Terminal with id {terminalId} not found");

        var debitCompany = await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId);
        if (debitCompany == null)
            throw new Exception($"Bank with id {terminal.DebitCompanyId} not found");

        return new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany
        };
    }

    public async Task<TransactionContext> CreateAsync(int terminalId, short? paymentMethodId)
    {
        if (!paymentMethodId.HasValue)
            return await CreateAsync(terminalId);

        var terminal = await _terminalRepository.GetByIdAsync(terminalId);
        if (terminal == null)
            throw new Exception($"Terminal with id {terminalId} not found");

        var debitCompany = await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId);
        if (debitCompany == null)
            throw new Exception($"Bank with id {terminal.DebitCompanyId} not found");

        var paymentMethod = await _paymentMethodRepository.GetByIdAsync(paymentMethodId.Value);
        if (paymentMethod == null)
            throw new Exception($"Payment method with id {paymentMethodId} not found");

        return new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany,
            PaymentMethod = paymentMethod
        };
    }

    private async Task<TransactionContext?> CreateTransactionContextFromReferenceAsync(string referenceCode)
    {
        var fail = await _context.TblCompanyTransFails
            .Include(t => t.CreditCard)
            .ThenInclude(c => c.BillingAddress)
            .FirstOrDefaultAsync(t => t.DebitReferenceCode == referenceCode);
        if (fail != null)
            return await CreateFromFailAsync(fail);

        var pass = await _context.TblCompanyTransPasses
            .Include(t => t.CreditCard)
            .ThenInclude(c => c.BillingAddress)
            .FirstOrDefaultAsync(t => t.DebitReferenceCode == referenceCode);
        if (pass != null)
            return await CreateFromPassAsync(pass);

        var approved = await _context.TblCompanyTransApprovals
            .Include(t => t.CreditCard)
            .ThenInclude(c => c.BillingAddress)
            .FirstOrDefaultAsync(t => t.DebitReferenceCode == referenceCode);
        if (approved != null)
            return await CreateFromApprovalAsync(approved);

        var pending = await _context.TblCompanyTransPendings
            .Include(t => t.CreditCard)
            .ThenInclude(c => c.BillingAddress)
            .FirstOrDefaultAsync(t => t.DebitReferenceCode == referenceCode);
        if (pending != null)
            return await CreateFromPendingAsync(pending);

        return null;
    }

    private async Task<TransactionContext> CreateFromFailAsync(Ef.Entities.TblCompanyTransFail trx)
    {
        var terminal = await _terminalRepository.GetByTerminalNumberAsync(trx.TerminalNumber);
        var debitCompany = terminal != null
            ? await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId)
            : null;

        var context = new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany,
            IsFinalized = true,
            TrxId = trx.Id,
            TrxDate = trx.InsertDate,
            ApprovalNumber = trx.ApprovalNumber ?? string.Empty,
            ReplyCode = trx.ReplyCode,
            DebitRefCode = trx.DebitReferenceCode,
            DebitRefNum = trx.DebitReferenceNum ?? string.Empty,
            Amount = trx.Amount,
            Payments = trx.Payments,
            OrderId = trx.OrderNumber,
            ClientIp = trx.Ipaddress,
            TransType = trx.TransType,
            CreditType = trx.CreditType,
            ErrorMessage = trx.DebitDeclineReason ?? string.Empty
        };

        await PopulateCreditCardDataAsync(context, trx.CreditCardId);
        return context;
    }

    private async Task<TransactionContext> CreateFromPassAsync(Ef.Entities.TblCompanyTransPass trx)
    {
        var terminal = await _terminalRepository.GetByTerminalNumberAsync(trx.TerminalNumber);
        var debitCompany = terminal != null
            ? await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId)
            : null;

        var context = new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany,
            IsFinalized = true,
            TrxId = trx.Id,
            TrxDate = trx.InsertDate,
            ApprovalNumber = trx.ApprovalNumber ?? string.Empty,
            ReplyCode = trx.ReplyCode,
            DebitRefCode = trx.DebitReferenceCode,
            DebitRefNum = trx.DebitReferenceNum ?? string.Empty,
            Amount = trx.Amount,
            Payments = trx.Payments,
            OrderId = trx.OrderNumber,
            ClientIp = trx.Ipaddress,
            CreditType = trx.CreditType
        };

        await PopulateCreditCardDataAsync(context, trx.CreditCardId);
        return context;
    }

    private async Task<TransactionContext> CreateFromApprovalAsync(Ef.Entities.TblCompanyTransApproval trx)
    {
        var terminal = await _terminalRepository.GetByTerminalNumberAsync(trx.TerminalNumber);
        var debitCompany = terminal != null
            ? await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId)
            : null;

        var context = new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany,
            IsFinalized = true,
            TrxId = trx.Id,
            TrxDate = trx.InsertDate,
            ApprovalNumber = trx.ApprovalNumber ?? string.Empty,
            ReplyCode = trx.ReplyCode,
            DebitRefCode = trx.DebitReferenceCode,
            DebitRefNum = trx.DebitReferenceNum ?? string.Empty,
            Amount = trx.Amount,
            Payments = trx.Payments,
            OrderId = trx.OrderNumber,
            ClientIp = trx.Ipaddress,
            CreditType = trx.CreditType,
            PendingParams = trx.TextValue ?? string.Empty
        };

        await PopulateCreditCardDataAsync(context, trx.CreditCardId);
        return context;
    }

    private async Task<TransactionContext> CreateFromPendingAsync(Ef.Entities.TblCompanyTransPending trx)
    {
        var terminal = await _terminalRepository.GetByTerminalNumberAsync(trx.TerminalNumber);
        var debitCompany = terminal != null
            ? await _terminalRepository.GetDebitCompanyByIdAsync(terminal.DebitCompanyId)
            : null;

        var context = new TransactionContext(_domainConfiguration)
        {
            Terminal = terminal,
            DebitCompany = debitCompany,
            IsFinalized = false,
            TrxId = trx.Id,
            TrxDate = trx.InsertDate,
            ApprovalNumber = trx.DebitApprovalNumber ?? string.Empty,
            ReplyCode = trx.ReplyCode,
            DebitRefCode = trx.DebitReferenceCode,
            DebitRefNum = trx.DebitReferenceNum ?? string.Empty,
            Amount = trx.TransAmount,
            Payments = trx.TransPayments,
            OrderId = trx.OrderNumber,
            ClientIp = trx.IpAddress,
            TransType = trx.TransType,
            CreditType = trx.TransCreditType,
            PayFor = trx.PayforText ?? string.Empty,
            PendingParams = trx.TextValue ?? string.Empty
        };

        await PopulateCreditCardDataAsync(context, trx.CreditCardId);
        return context;
    }

    private async Task PopulateCreditCardDataAsync(TransactionContext context, int? creditCardId)
    {
        if (creditCardId == null) return;

        var card = await _context.TblCreditCards
            .Include(c => c.BillingAddress)
            .FirstOrDefaultAsync(c => c.Id == creditCardId);

        if (card != null)
        {
            context.PayerName = card.Member ?? string.Empty;
            context.Email = card.Email ?? string.Empty;
            context.BinCountry = card.Bincountry ?? string.Empty;
        }
    }

    private OperationType GetOperationType(ChargeAttempt attemptLog, TransactionContext locatedTrx)
    {
        var param = attemptLog.QueryString + "|" + attemptLog.RequestForm;
        var paramSplit = param.Split('|');
        string? requestSource = null;
        string? recurring1 = null;

        foreach (var currentParam in paramSplit)
        {
            var currentParamSplit = currentParam.Split(['='], 2);
            if (currentParamSplit.Length < 2) continue;

            var currentKey = currentParamSplit[0].Trim().ToLower();

            switch (currentKey)
            {
                case "requestsource":
                    requestSource = currentParamSplit[1];
                    break;
                case "recurring1":
                    recurring1 = currentParamSplit[1];
                    break;
                case "returl":
                    locatedTrx.RedirectUrl = currentParamSplit[1];
                    break;
                case "l3d_arrival_date":
                    locatedTrx.Level3DataArrivalDate = currentParamSplit[1];
                    break;
            }
        }

        var is3DSecure = (locatedTrx.Terminal?.Enable3DSecure ?? false) ||
                         (!string.IsNullOrEmpty(attemptLog.QueryString) &&
                          attemptLog.QueryString.Contains("paymentMethodVar=GPY"));

        var isRecurring = requestSource == "20" || !string.IsNullOrEmpty(recurring1);
        if (isRecurring)
        {
            if (string.IsNullOrEmpty(recurring1))
                return OperationType.RecurringSale;

            return is3DSecure ? OperationType.RecurringInit3DS : OperationType.RecurringInit;
        }

        if (locatedTrx.CreditType == 1 || locatedTrx.CreditType == 8)
        {
            if (locatedTrx.TransType == 0 || locatedTrx.TransType == 3)
                return is3DSecure ? OperationType.Sale3DS : OperationType.Sale;
            else if (locatedTrx.TransType == 1)
                return is3DSecure ? OperationType.Authorization3DS : OperationType.Authorization;
            else if (locatedTrx.TransType == 2)
                return OperationType.AuthorizationCapture;
        }
        else
        {
            return OperationType.Refund;
        }

        return OperationType.Unknown;
    }
}