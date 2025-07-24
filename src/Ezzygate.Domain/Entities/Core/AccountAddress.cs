using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

public sealed class AccountAddress
{
    [Key]
    public int AccountAddressId { get; set; }

    [StringLength(100)]
    public string? Street1 { get; set; }

    [StringLength(100)]
    public string? Street2 { get; set; }

    [StringLength(60)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? PostalCode { get; set; }

    [StringLength(2)]
    public string? StateISOCode { get; set; }

    [Required]
    [StringLength(2)]
    public string CountryISOCode { get; set; } = string.Empty;

    public string FullAddress => 
        $"{Street1}{(!string.IsNullOrEmpty(Street2) ? $", {Street2}" : "")}, {City}, {StateISOCode} {PostalCode}, {CountryISOCode}".Trim();

    public CountryList? Country { get; set; }
    public StateList? State { get; set; }

    public ICollection<Account> AccountsAsPersonalAddress { get; set; } = new List<Account>();
    public ICollection<Account> AccountsAsBusinessAddress { get; set; } = new List<Account>();
    public ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();
    public ICollection<CustomerShippingDetail> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetail>();
}