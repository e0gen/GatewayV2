namespace Ezzygate.WebApi.Dtos.Merchants.CreditCard;

public class AddressDto
{
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? StateIso { get; set; }
    public string? CountryIso { get; set; }
}