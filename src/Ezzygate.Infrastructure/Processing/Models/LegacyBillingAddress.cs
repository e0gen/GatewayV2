namespace Ezzygate.Infrastructure.Processing.Models;

public class LegacyBillingAddress
{
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? StateIso { get; set; }
    public string? CountryIso { get; set; }
}