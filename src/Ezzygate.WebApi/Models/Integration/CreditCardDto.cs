namespace Ezzygate.WebApi.Models.Integration;

public class CreditCardDto
{
    public BillingAddressDto? BillingAddress { get; set; }
    public string? Number { get; set; }
    public string? Cvv { get; set; }
    public string? Track2 { get; set; }
    public int? ExpirationMonth { get; set; }
    public int? ExpirationYear { get; set; }
    public string? HolderName { get; set; }
}