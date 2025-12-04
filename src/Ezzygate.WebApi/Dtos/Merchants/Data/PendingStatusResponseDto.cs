namespace Ezzygate.WebApi.Dtos.Merchants.Data;

public class PendingStatusResponseDto
{
    public int TransactionId { get; set; }
    public string Status { get; set; } = string.Empty;
}