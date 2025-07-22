using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// State list lookup entity
/// </summary>
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

    // Navigation properties
    public CountryList Country { get; set; } = null!;

    // Collections
    public ICollection<Core.AccountAddress> AccountAddresses { get; set; } = new List<Core.AccountAddress>();
} 