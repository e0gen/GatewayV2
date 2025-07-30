namespace Ezzygate.WebApi.Models;

public class CreditCard
{
    public CreditCard()
    {
    }

    public CreditCard(string cardNumber, int expMM, int expYY)
    {
        Number = cardNumber;
        ExpirationMonth = expMM;
        ExpirationYear = expYY;
    }

    public string? Number { get; set; }

    public int ExpirationMonth { get; set; }

    public int ExpirationYear { get; set; }

    public string? HolderName { get; set; }

    public string? Cvv { get; set; }

    public string? Track2 { get; set; }

    public string? Type { get; set; }

    public Address? BillingAddress { get; set; }
} 