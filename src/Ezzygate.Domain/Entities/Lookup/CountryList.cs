using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Country list lookup entity
/// </summary>
public sealed class CountryList
{
    [Key]
    [StringLength(2)]
    public string CountryISOCode { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Name { get; set; }
    
    public bool IsActive { get; set; }

    // Collections
    public ICollection<Core.AccountAddress> AccountAddresses { get; set; } = new List<Core.AccountAddress>();
    public ICollection<StateList> States { get; set; } = new List<StateList>();
} 