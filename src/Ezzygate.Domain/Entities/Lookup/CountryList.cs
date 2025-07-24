using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class CountryList
{
    [Key]
    [StringLength(2)]
    public string CountryISOCode { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Name { get; set; }

    public ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();
    public ICollection<StateList> States { get; set; } = new List<StateList>();

    public ICollection<AccountPaymentMethod> AccountPaymentMethodsAsIssuerCountry { get; set; } = new List<AccountPaymentMethod>();
}