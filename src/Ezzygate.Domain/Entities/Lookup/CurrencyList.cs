using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Currency list lookup entity
/// </summary>
public sealed class CurrencyList
{
    [Key]
    [StringLength(3)]
    public string CurrencyISOCode { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Name { get; set; }
    
    [StringLength(5)]
    public string? Symbol { get; set; }
    
    public bool IsActive { get; set; }

    // Collections
    public ICollection<Core.Account> Accounts { get; set; } = new List<Core.Account>();
    public ICollection<Core.AccountBalance> AccountBalances { get; set; } = new List<Core.AccountBalance>();
} 