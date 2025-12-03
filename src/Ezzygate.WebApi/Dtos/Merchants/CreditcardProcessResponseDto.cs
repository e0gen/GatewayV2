namespace Ezzygate.WebApi.Dtos.Merchants;

public class CreditcardProcessResponseDto
{
    public CreditCardDto? CreditCard { get; set; }
    public CustomerDto? Customer { get; set; }
    public RecurringDto? Recurring { get; set; }
    public Level3DataDto? Level3Data { get; set; }
    public string? RecurringSeriesId { get; set; }
    public decimal Amount { get; set; }
    public byte Installments { get; set; }
    public string CurrencyIso { get; set; } = string.Empty;
    public string? PostRedirectUrl { get; set; }
    public string? AuthenticationRedirectUrl { get; set; }
    public bool SaveCard { get; set; }
    public string? SavedCardId { get; set; }
    public bool AuthorizeOnly { get; set; }
    public int? TrmCode { get; set; }
    public string? ReplyCode { get; set; }
    public string? ReplyDescription { get; set; }
    public string? TransactionId { get; set; }
    public string? Descriptor { get; set; }
    public string? Order { get; set; }
    public string? Comment { get; set; }
    public string? OrderDescription { get; set; }
    public string? ClientIP { get; set; }
    public int TransType { get; set; }
    public string? Date { get; set; }
    public string? ConfirmationNumber { get; set; }
    public string? MerchantID { get; set; }
    public string? ClientEmail { get; set; }
    public string? ClientPhoneNumber { get; set; }
    public string? ClientFullName { get; set; }
    public string? ClientWalletId { get; set; }
    public string? ApprovalNumber { get; set; }
    public string? AcquirerReferenceNumber { get; set; }
    public string? ClientId { get; set; }
    public string? TransRefNum { get; set; }
    public string? Signature { get; set; }
    public string? TransMaxInstallments { get; set; }
    public string? DispRecurring { get; set; }
    public string? DispMobile { get; set; }
    public string? DebugTest { get; set; }
    public decimal TipAmount { get; set; }
    public bool CardPresent { get; set; }
}