namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyRefundRequest
{
    public string MerchantNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string CurrencyIso { get; set; } = string.Empty;
    public int TransactionId { get; set; }
    public string? ClientIP { get; set; }
}