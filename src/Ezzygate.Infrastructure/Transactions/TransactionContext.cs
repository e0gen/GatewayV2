using Ezzygate.Domain.Enums;
using Ezzygate.Domain.Models;

namespace Ezzygate.Infrastructure.Transactions;

public class TransactionContext
{
    public OperationType OpType { get; set; }

    public bool Is3ds => OpType == OperationType.Authorization3Ds ||
                         OpType == OperationType.Sale3Ds ||
                         OpType == OperationType.RecurringInit3Ds;

    public TransactionContext? LocatedTrx { get; init; }
    public Terminal? Terminal { get; init; }
    public DebitCompany? DebitCompany { get; init; }
    public PaymentMethod? PaymentMethod { get; init; }
    public int ChargeAttemptLogId { get; set; }
    public string DebitRefCode { get; set; } = string.Empty;
    public string? SentDebitRefCode { get; set; } = string.Empty;
    public string? ApprovalNumber { get; set; } = string.Empty;
    public string? DebitRefNum { get; set; } = string.Empty;
    public bool IsTestTerminal => Terminal?.IsTestTerminal ?? false;
    public string TerminalNumber => Terminal?.TerminalNumber ?? string.Empty;
    public string TerminalAccount => Terminal?.AccountId ?? string.Empty;
    public string TerminalAccount3D => Terminal?.AccountId3D ?? string.Empty;
    public string TerminalSubAccount => Terminal?.AccountSubId ?? string.Empty;
    public string TerminalSubAccount3D => Terminal?.AccountSubId3D ?? string.Empty;
    public string TerminalAuthCode => Terminal?.AuthenticationCode1 ?? string.Empty;
    public string TerminalAuthCode3D => Terminal?.AuthenticationCode3D ?? string.Empty;
    public bool IsAutomatedRequest { get; set; }
    public string? AutomatedStatus { get; set; } = string.Empty;
    public string? AutomatedErrorMessage { get; set; } = string.Empty;
    public object? AutomatedPayload { get; set; }
    public int TrxId { get; set; }
    public DateTime TrxDate { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string Level3DataArrivalDate { get; set; } = string.Empty;
    public string PendingParams { get; set; } = string.Empty;
    public bool IsFinalized { get; set; }
    public string ReplyCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public string BinCountry { get; set; } = string.Empty;
    public string PayFor { get; set; } = string.Empty;
    public string? RequestContent { get; set; } = string.Empty;
    public string? FormData { get; set; } = string.Empty;
    public string? QueryString { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string CurrencyIso { get; set; } = string.Empty;
    public byte Payments { get; set; }
    public string PayerName { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string Cvv { get; set; } = string.Empty;
    public string Track2 { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string PersonalIdNumber { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? ClientIp { get; set; } = string.Empty;
    public string? MerchantNumber { get; set; } = string.Empty;
    public string? OrderId { get; set; } = string.Empty;
    public string? CartId { get; set; } = string.Empty;
    public string? CustomerId { get; set; }
    public decimal OriginalAmount { get; set; }
    public TransactionSource? RequestSource { get; set; }
    public int TransType { get; set; }
    public int CreditType { get; set; }
    public string? Comment { get; set; } = string.Empty;
    public string? RoutingNumber { get; set; } = string.Empty;
    public string? AccountNumber { get; set; } = string.Empty;
    public string? AccountName { get; set; } = string.Empty;
    public bool IsMobileMoto { get; set; }
}