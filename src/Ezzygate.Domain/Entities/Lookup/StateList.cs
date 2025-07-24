using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

public sealed class StateList
{
    [Key]
    [StringLength(2)]
    public string StateISOCode { get; set; } = string.Empty;

    [Required]
    [StringLength(2)]
    public string CountryISOCode { get; set; } = string.Empty;

    [StringLength(50)]
    public string? Name { get; set; }

    public CountryList Country { get; set; } = null!;

    public ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();
}