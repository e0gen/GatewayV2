using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Enums;

namespace Ezzygate.WebApi.Dtos.Merchants.Data;

public class TransactionsRequestDto
{
    public int? TransactionId { get; set; }
    
    [Required]
    public TransactionStatusType Status { get; set; }
    
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}