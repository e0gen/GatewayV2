using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants;

public class CreditcardProcessRequestDto
{
    public CreditCardDto? CreditCard { get; set; }
    public CustomerDto? Customer { get; set; }
    public RecurringDto? Recurring { get; set; }
    public Level3DataDto? Level3Data { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required, Range(1, 12)]
    public byte Installments { get; set; }

    [Required, MaxLength(3), MinLength(3)]
    public string CurrencyIso { get; set; } = string.Empty;

    public string? PostRedirectUrl { get; set; }
    public bool SaveCard { get; set; }
    public string? SavedCardId { get; set; }
    public bool AuthorizeOnly { get; set; }
    public int? TrmCode { get; set; }
    public string? Order { get; set; }
    public string? Comment { get; set; }
    public string? OrderDescription { get; set; }
    public string? ClientIP { get; set; }
    public decimal TipAmount { get; set; }
    public bool CardPresent { get; set; }
}