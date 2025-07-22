using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Account type lookup entity
/// </summary>
public sealed class AccountType
{
    [Key]
    public byte AccountTypeId { get; set; }
    
    [StringLength(50)]
    public string? Name { get; set; }

    // Collections
    public ICollection<Core.Account> Accounts { get; set; } = new List<Core.Account>();
} 