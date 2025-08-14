namespace Ezzygate.WebApi.Models.Integration;

public class BillingAddressDto
{
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? CountryIso { get; set; }
    public string? PostalCode { get; set; }
    public string? StateIso { get; set; }
}