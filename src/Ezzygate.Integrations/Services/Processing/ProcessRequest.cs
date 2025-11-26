using Ezzygate.Domain.Enums;

namespace Ezzygate.Integrations.Services.Processing;

public sealed class ProcessRequest
{
    public OperationType OperationType { get; init; }
    public int TerminalId { get; init; }
    public short? PaymentMethodId { get; init; }
    public string? DebitRefCode { get; init; }
    public string? DebitRefNum { get; init; }
    public string? ApprovalNumber { get; init; }
    public decimal Amount { get; init; }
    public decimal OriginalAmount { get; init; }
    public string CurrencyIso { get; init; } = string.Empty;
    public byte Payments { get; init; }
    public int TransType { get; init; }
    public int CreditType { get; init; }
    public string? CardHolderName { get; init; }
    public string? CardNumber { get; init; }
    public string? Cvv { get; init; }
    public string? Track2 { get; init; }
    public int ExpirationMonth { get; init; }
    public int ExpirationYear { get; init; }
    public string? Email { get; init; }
    public string? PersonalIdNumber { get; init; }
    public string? PhoneNumber { get; init; }
    public string? MerchantNumber { get; init; }
    public string? OrderId { get; init; }
    public string? CartId { get; init; }
    public string? CustomerId { get; init; }
    public string? Comment { get; init; }
    public string? RoutingNumber { get; init; }
    public string? AccountNumber { get; init; }
    public string? AccountName { get; init; }
    public string? ClientIp { get; init; }
    public string? RequestContent { get; init; }
    public string? FormData { get; init; }
    public string? QueryString { get; init; }
    public TransactionSource? RequestSource { get; init; }
    public bool IsAutomatedRequest { get; init; }
    public string? AutomatedStatus { get; init; }
    public string? AutomatedErrorMessage { get; init; }
    public object? AutomatedPayload { get; init; }
}