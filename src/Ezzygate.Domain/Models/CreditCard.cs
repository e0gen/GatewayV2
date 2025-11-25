using Ezzygate.Domain.Enums;

namespace Ezzygate.Domain.Models;

public class CreditCard
{
    public string CardNumber { get; set; } = string.Empty;
    public PaymentMethodEnum PaymentMethodId { get; set; }
    public string? BinCountryIsoCode { get; set; }
    public string? BrandName { get; set; }

    public string MaskedNumber
    {
        get
        {
            if (string.IsNullOrEmpty(CardNumber)) return string.Empty;
            return CardNumber.Length switch
            {
                > 10 => string.Concat(CardNumber.AsSpan(0, 6), new string('X', CardNumber.Length - 10),
                    CardNumber.AsSpan(CardNumber.Length - 4, 4)),
                > 6 => string.Concat(CardNumber.AsSpan(0, 4), new string('X', CardNumber.Length - 6),
                    CardNumber.AsSpan(CardNumber.Length - 2, 2)),
                _ => new string('X', CardNumber.Length)
            };
        }
    }

    public string Last4
    {
        get
        {
            if (string.IsNullOrEmpty(CardNumber) || CardNumber.Length < 4)
                return CardNumber;
            return CardNumber[^4..];
        }
    }

    public string First6
    {
        get
        {
            if (string.IsNullOrEmpty(CardNumber) || CardNumber.Length < 6)
                return CardNumber;
            return CardNumber[..6];
        }
    }

    public string[] ToGroupedString()
    {
        var result = new List<string>();
        for (var i = 0; i < CardNumber.Length; i += 4)
            result.Add(CardNumber.Substring(i, Math.Min(4, CardNumber.Length - i)));
        return result.ToArray();
    }
}