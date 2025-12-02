namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyPaymentResult
{
    public string ReplyCode { get; set; } = string.Empty;
    public string ReplyDescription { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public string? Last4 { get; set; }
    public string? CardType { get; set; }
    public string? SavedCardId { get; set; }
    public string? Descriptor { get; set; }
    public string? AuthenticationRedirectUrl { get; set; }
    public string? RecurringSeries { get; set; }
    public string? Order { get; set; }
    public string? Comment { get; set; }
    public string? Date { get; set; }
    public string? ConfirmationNumber { get; set; }
    public string? ApprovalNumber { get; set; }
    public string? AcquirerReferenceNumber { get; set; }
    public string? TransRefNum { get; set; }
    public string? WalletId { get; set; }
    public string? Signature { get; set; }
    public string? Payments { get; set; }

    public string GetResultStatus()
    {
        return ReplyCode switch
        {
            "000" => "Approved",
            "553" => "Pending",
            _ => "Declined"
        };
    }
}