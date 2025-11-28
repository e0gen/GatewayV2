namespace Ezzygate.Integrations.Core.Models;

public class CreditCard
{
    public required string Number { get; set; }
    public required int ExpirationMonth { get; set; }
    public required int ExpirationYear { get; set; }
    public required string Cvv { get; set; }
    public string? Track2 { get; set; }
    public required string HolderName { get; set; }
    public string? Type { get; set; }
    public BillingAddress? BillingAddress { get; set; }
}