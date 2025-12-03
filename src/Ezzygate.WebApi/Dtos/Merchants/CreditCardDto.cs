namespace Ezzygate.WebApi.Dtos.Merchants;

public class CreditCardDto
{
    public string? Number { get; set; }
    public int ExpirationMonth { get; set; }
    public int ExpirationYear { get; set; }
    public string? HolderName { get; set; }
    public string? Cvv { get; set; }
    public string? Track2 { get; set; }
    public string? Type { get; set; }
    public AddressDto? BillingAddress { get; set; }
}