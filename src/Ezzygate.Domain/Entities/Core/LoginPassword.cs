using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Login password entity for storing encrypted passwords
/// </summary>
public sealed class LoginPassword
{
    [Key]
    public int LoginPasswordId { get; set; }
    
    [Required]
    public int LoginAccountId { get; set; }
    
    [Required]
    public DateTime InsertDate { get; set; }
    
    [Required]
    [MaxLength(100)]
    public byte[] PasswordEncrypted { get; set; } = Array.Empty<byte>();
    
    [Required]
    public byte EncryptionKey { get; set; }
    
    [Required]
    public bool IsTemporary { get; set; }

    // Computed properties
    public bool IsExpired => IsTemporary && InsertDate.AddDays(1) < DateTime.UtcNow; // Temp passwords expire after 1 day

    // Navigation properties
    public LoginAccount LoginAccount { get; set; } = null!;
} 