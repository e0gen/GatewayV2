namespace Ezzygate.WebApi.Dtos.Merchants;

public class CaptureResponseDto
{
    public string CurrencyIso { get; set; } = string.Empty;
    public string? ReplyCode { get; set; }
    public string? ReplyDescription { get; set; }
    public string TransactionId { get; set; } = string.Empty;
}