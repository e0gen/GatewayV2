using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants;

public class CaptureRequestDto
{
    [Required, MaxLength(3), MinLength(3)]
    public string CurrencyIso { get; set; } = string.Empty;

    [Required]
    public string TransactionId { get; set; } = string.Empty;
}