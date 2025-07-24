using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

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
    public byte[] PasswordEncrypted { get; set; } = [];

    [Required]
    public byte EncryptionKey { get; set; }

    [Required]
    public bool IsTemporary { get; set; }

    public bool IsExpired => IsTemporary && InsertDate.AddDays(1) < DateTime.UtcNow;

    public LoginAccount LoginAccount { get; set; } = null!;
}