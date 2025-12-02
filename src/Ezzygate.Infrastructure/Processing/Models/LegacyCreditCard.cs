namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyCreditCard
{
    public string? Number { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string? HolderName { get; set; }
    public string? Cvv { get; set; }
    public string? Type { get; set; }
    public LegacyBillingAddress? BillingAddress { get; set; }
}