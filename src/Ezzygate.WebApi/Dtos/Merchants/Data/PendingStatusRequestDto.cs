using System.ComponentModel.DataAnnotations;

namespace Ezzygate.WebApi.Dtos.Merchants.Data;

public class PendingStatusRequestDto
{
    [Required]
    public int TransactionId { get; set; }
}