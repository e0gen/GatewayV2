namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyProcessRequest
{
    public string MerchantNumber { get; set; } = string.Empty;
    public LegacyCreditCard? CreditCard { get; set; }
    public LegacyCustomer? Customer { get; set; }
    public LegacyRecurring? Recurring { get; set; }
    public LegacyLevel3Data? Level3Data { get; set; }
    public decimal Amount { get; set; }
    public byte Installments { get; set; }
    public string CurrencyIso { get; set; } = string.Empty;
    public int TransType { get; set; }
    public int TypeCredit { get; set; }
    public string? PostRedirectUrl { get; set; }
    public int StoreCc { get; set; }
    public string? SavedCardId { get; set; }
    public int? TrmCode { get; set; }
    public string? Order { get; set; }
    public string? Comment { get; set; }
    public string? OrderDescription { get; set; }
    public string? ClientIP { get; set; }
    public decimal TipAmount { get; set; }
    public bool CardPresent { get; set; }
}