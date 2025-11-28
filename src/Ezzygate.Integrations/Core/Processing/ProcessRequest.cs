using Ezzygate.Domain.Enums;
using Ezzygate.Integrations.Core.Models;

namespace Ezzygate.Integrations.Core.Processing;

public sealed class ProcessRequest
{
    public OperationType OperationType { get; init; }
    public string? DebitRefCode { get; init; }
    public string? DebitRefNum { get; init; }
    public string? ApprovalNumber { get; init; }
    public int TerminalId { get; init; }
    public bool Is3DSecure { get; set; }
    public required CreditCard CreditCard { get; init; }
    public required Customer Customer { get; set; }
    public short? PaymentMethodId { get; init; }
    public decimal Amount { get; init; }
    public decimal OriginalAmount { get; init; }
    public required string CurrencyIso { get; init; }
    public byte Payments { get; init; }
    public int TransType { get; init; }
    public int CreditType { get; init; }
    public string? MerchantNumber { get; init; }
    public string? OrderId { get; init; }
    public string? CartId { get; init; }
    public string? CustomerId { get; init; }
    public int? ChargeAttemptLogId { get; init; }
    public string? Comment { get; init; }
    public string? RoutingNumber { get; init; }
    public string? AccountNumber { get; init; }
    public string? AccountName { get; init; }
    public string? ClientIp { get; init; }
    public string? RequestContent { get; init; }
    public string? FormData { get; init; }
    public string? QueryString { get; init; }
    public TransactionSource? RequestSource { get; init; }
}