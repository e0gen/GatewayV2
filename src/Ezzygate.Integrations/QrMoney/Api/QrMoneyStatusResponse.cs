using System.Text.Json.Serialization;
using Ezzygate.Integrations.QrMoney.Models;

namespace Ezzygate.Integrations.QrMoney.Api;

public class QrMoneyStatusResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("recipientName")]
    public string? RecipientName { get; set; }

    [JsonPropertyName("paymentRequestStatusId")]
    public int PaymentRequestStatusId { get; set; } // 0 – waiting, 1 – paid, 2 – unpaid, 3 – cancelled, 4 – pending, 5 – archived

    [JsonPropertyName("transactionId")]
    public string? TransactionId { get; set; }

    [JsonPropertyName("transactionNumber")]
    public string? TransactionNumber { get; set; }

    [JsonPropertyName("transactionStatusId")]
    public int TransactionStatusId { get; set; } // 0 – waiting, 1 – approved, 2 – declined, 3 – pending

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("unit")]
    public string? Unit { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("referenceId")]
    public string? ReferenceId { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("client")]
    public Client? Client { get; set; }

    [JsonPropertyName("notifyUrl")]
    public string? NotifyUrl { get; set; }

    [JsonPropertyName("successUrl")]
    public string? SuccessUrl { get; set; }

    [JsonPropertyName("failureUrl")]
    public string? FailureUrl { get; set; }

    [JsonPropertyName("lastLog")]
    public Log? LastLog { get; set; }
}