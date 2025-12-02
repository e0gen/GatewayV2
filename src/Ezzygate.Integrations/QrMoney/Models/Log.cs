using System.Text.Json.Serialization;

namespace Ezzygate.Integrations.QrMoney.Models;

public class Log
{
    [JsonPropertyName("transactionStatusId")]
    public int TransactionStatusId { get; set; } // 0 – waiting, 1 – approved, 2 – declined, 3 – pending

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}