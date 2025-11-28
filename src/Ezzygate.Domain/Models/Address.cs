namespace Ezzygate.Domain.Models;

public class Address : IAddress
{
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? StateIsoCode { get; set; }
    public string? CountryIsoCode { get; set; }

    public static void Copy(IAddress src, IAddress dest)
    {
        dest.AddressLine1 = src.AddressLine1;
        dest.AddressLine2 = src.AddressLine2;
        dest.City = src.City;
        dest.PostalCode = src.PostalCode;
        dest.StateIsoCode = src.StateIsoCode;
        dest.CountryIsoCode = src.CountryIsoCode;
    }

    public static bool IsEmpty(IAddress address)
    {
        return
            string.IsNullOrEmpty(address.AddressLine1) &&
            string.IsNullOrEmpty(address.AddressLine2) &&
            string.IsNullOrEmpty(address.City) &&
            string.IsNullOrEmpty(address.PostalCode) &&
            string.IsNullOrEmpty(address.StateIsoCode) &&
            string.IsNullOrEmpty(address.CountryIsoCode);
    }
}

public interface IAddress
{
    string AddressLine1 { get; set; }
    string AddressLine2 { get; set; }
    string City { get; set; }
    string PostalCode { get; set; }
    string StateIsoCode { get; set; }
    string CountryIsoCode { get; set; }
}