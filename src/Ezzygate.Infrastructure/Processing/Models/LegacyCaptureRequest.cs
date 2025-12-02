namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyCaptureRequest
{
    public string MerchantNumber { get; set; } = string.Empty;
    public string CurrencyIso { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string? ClientIP { get; set; }
}