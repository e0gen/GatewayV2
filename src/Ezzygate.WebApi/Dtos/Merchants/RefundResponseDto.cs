namespace Ezzygate.WebApi.Dtos.Merchants;

public class RefundResponseDto
{
    public decimal Amount { get; set; }
    public string CurrencyIso { get; set; } = string.Empty;
    public string? ReplyCode { get; set; }
    public string? ReplyDescription { get; set; }
    public int TransactionId { get; set; }
    public string? Comment { get; set; }
}