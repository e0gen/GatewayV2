using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("LoginPassword", Schema = "Data")]
public partial class LoginPassword
{
    [Key]
    [Column("LoginPassword_id")]
    public int LoginPasswordId { get; set; }

    [Column("LoginAccount_id")]
    public int LoginAccountId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [MaxLength(100)]
    public byte[] PasswordEncrypted { get; set; } = null!;

    public byte EncryptionKey { get; set; }

    public bool IsTemporary { get; set; }

    [ForeignKey("LoginAccountId")]
    [InverseProperty("LoginPasswords")]
    public virtual LoginAccount LoginAccount { get; set; } = null!;
}
