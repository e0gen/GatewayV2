using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Login account entity for user authentication
/// </summary>
public sealed class LoginAccount
{
    [Key]
    public int LoginAccountId { get; set; }
    
    [Required]
    public byte LoginRoleId { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
    
    [StringLength(30)]
    public string? LoginUser { get; set; }
    
    [StringLength(50)]
    public string? LoginEmail { get; set; }
    
    public byte? FailCount { get; set; }
    
    public DateTime? LastFailTime { get; set; }
    
    public DateTime? LastSuccessTime { get; set; }
    
    public DateTime? BlockEndTime { get; set; }

    // Computed properties
    public bool IsBlocked => BlockEndTime.HasValue && BlockEndTime > DateTime.UtcNow;
    public bool IsLocked => FailCount >= 5; // Assuming 5 is the max fail count

    // Navigation properties
    public LoginRole? LoginRole { get; set; }

    // Collections
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<AccountSubUser> AccountSubUsers { get; set; } = new List<AccountSubUser>();
    public ICollection<LoginPassword> LoginPasswords { get; set; } = new List<LoginPassword>();
} 