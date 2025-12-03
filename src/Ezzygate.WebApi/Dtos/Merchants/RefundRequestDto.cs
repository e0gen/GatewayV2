using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants;

public class RefundRequestDto
{
    [Required]
    public decimal Amount { get; set; }

    [Required, MaxLength(3), MinLength(3)]
    public string CurrencyIso { get; set; } = string.Empty;

    [Required]
    public int TransactionId { get; set; }

    public string? Comment { get; set; }
}