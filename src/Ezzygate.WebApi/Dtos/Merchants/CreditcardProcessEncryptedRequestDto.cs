using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants;

public class CreditcardProcessEncryptedRequestDto
{
    [Required]
    public string Data { get; set; } = string.Empty;
}