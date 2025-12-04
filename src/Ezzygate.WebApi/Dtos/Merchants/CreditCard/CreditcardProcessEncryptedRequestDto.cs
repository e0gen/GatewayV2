using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants.CreditCard;

public class CreditcardProcessEncryptedRequestDto
{
    [Required]
    public string Data { get; set; } = string.Empty;
}