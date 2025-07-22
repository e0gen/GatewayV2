using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Balance source type lookup entity
/// </summary>
public sealed class BalanceSourceType
{
    [Key]
    [StringLength(30)]
    public string BalanceSourceTypeId { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string? Name { get; set; }
    
    [Required]
    public bool IsFee { get; set; }

    // Collections
    public ICollection<Core.AccountBalance> AccountBalances { get; set; } = new List<Core.AccountBalance>();
} 