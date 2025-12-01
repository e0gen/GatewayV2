using System.Text.Json.Serialization;
using Ezzygate.Integrations.Extensions;

namespace Ezzygate.Integrations.Paysafe.Models;

public class OrderItemDetails
{
    [JsonPropertyName("preOrderItemAvailabilityDate")]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateTimeOffset PreOrderItemAvailabilityDate { get; set; }

    [JsonPropertyName("preOrderPurchaseIndicator")]
    public string? PreOrderPurchaseIndicator { get; set; }

    [JsonPropertyName("reorderItemsIndicator")]
    public string? ReorderItemsIndicator { get; set; }

    [JsonPropertyName("shippingIndicator")]
    public string? ShippingIndicator { get; set; }
}