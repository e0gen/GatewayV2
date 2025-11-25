using Ezzygate.Domain.Enums;

namespace Ezzygate.Domain.Models;

public class CreditCardBinInfo
{
    public int BinId { get; set; }
    public string Bin { get; set; } = string.Empty;
    public string CountryIsoCode { get; set; } = string.Empty;
    public PaymentMethodEnum PaymentMethodId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public int? BinLength { get; set; }
    public int? BinNumber { get; set; }
    public bool IsPrepaid { get; set; }
}