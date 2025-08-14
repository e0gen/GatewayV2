using Ezzygate.Domain.Enums;

namespace Ezzygate.WebApi.Models.Integration;

public class IntegrationFinalizeRequest
{
    public OperationType OperationType { get; set; }
    public string? DebitRefCode { get; set; }
    public string? ApprovalNumber { get; set; }
    public string DebitRefNum { get; set; } = null!;
    public int TerminalId { get; set; }
    public bool Is3DSecure { get; set; }
    public CreditCardDto? CreditCard { get; set; }
    public CustomerDto? Customer { get; set; }
    public decimal Amount { get; set; }
    public decimal OriginalAmount { get; set; }
    public string CurrencyIso { get; set; } = null!;
    public byte Payments { get; set; }
    public string? RequestContent { get; set; }
    public string? FormData { get; set; }
    public string? QueryString { get; set; }
    public string? ClientIp { get; set; }
    public string? ClientReferrer { get; set; }
    public string? ClientUserAgent { get; set; }
    public string? MerchantNumber { get; set; }
    public string? CartId { get; set; }
    public string? OrderId { get; set; }
    public string? CustomerId { get; set; }
    public int ChargeAttemptLogId { get; set; }
    public TransactionSource? RequestSource { get; set; }
    public int TransType { get; set; }
    public int CreditType { get; set; }
    public string? Comment { get; set; }
    public short? PaymentMethodId { get; set; }
    public string? RoutingNumber { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccountName { get; set; }
    public bool IsAutomatedRequest { get; set; }
    public string? AutomatedStatus { get; set; }
    public string? AutomatedErrorMessage { get; set; }
    public string? AutomatedPayload { get; set; }
}
