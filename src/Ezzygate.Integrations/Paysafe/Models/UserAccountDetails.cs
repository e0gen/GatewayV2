

using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.Paysafe.Models;

public class UserAccountDetails
{
    [JsonPropertyName("addCardAttemptsForLastDay")]
    public long AddCardAttemptsForLastDay { get; set; }

    [JsonPropertyName("changedDate")]
    public DateTimeOffset ChangedDate { get; set; }

    [JsonPropertyName("changedRange")]
    public string? ChangedRange { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTimeOffset CreatedDate { get; set; }

    [JsonPropertyName("createdRange")]
    public string? CreatedRange { get; set; }

    [JsonPropertyName("passwordChangedDate")]
    public DateTimeOffset PasswordChangedDate { get; set; }

    [JsonPropertyName("passwordChangedRange")]
    public string? PasswordChangedRange { get; set; }

    [JsonPropertyName("paymentAccountDetails")]
    public PaymentAccountDetails? PaymentAccountDetails { get; set; }

    [JsonPropertyName("shippingDetailsUsage")]
    public ShippingDetailsUsage? ShippingDetailsUsage { get; set; }

    [JsonPropertyName("suspiciousAccountActivity")]
    public bool SuspiciousAccountActivity { get; set; }

    [JsonPropertyName("totalPurchasesSixMonthCount")]
    public long TotalPurchasesSixMonthCount { get; set; }

    [JsonPropertyName("transactionCountForPreviousDay")]
    public long TransactionCountForPreviousDay { get; set; }

    [JsonPropertyName("transactionCountForPreviousYear")]
    public long TransactionCountForPreviousYear { get; set; }

    [JsonPropertyName("userLogin")]
    public UserLogin? UserLogin { get; set; }
}