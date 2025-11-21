namespace Ezzygate.Infrastructure.Notifications;

public class NotificationResult
{
    public int StatusCode { get; set; }
    public int MerchantId { get; set; }
    public int? TransactionId { get; set; }
    public string? LogXml { get; set; }
    public bool IsSuccess { get; set; }
}