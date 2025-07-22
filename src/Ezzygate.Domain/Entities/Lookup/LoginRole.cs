using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Login role lookup entity
/// </summary>
public sealed class LoginRole
{
    [Key]
    public byte LoginRoleId { get; set; }
    
    [StringLength(50)]
    public string? Name { get; set; }

    // Collections
    public ICollection<Core.LoginAccount> LoginAccounts { get; set; } = new List<Core.LoginAccount>();
} 