using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountExternalService", Schema = "Data")]
public partial class AccountExternalService
{
    [Key]
    [Column("AccountExternalService_id")]
    public short AccountExternalServiceId { get; set; }

    [Column("ExternalServiceType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? ExternalServiceTypeId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("ProtocolType_id")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ProtocolTypeId { get; set; }

    public bool IsActive { get; set; }

    [Column("ServerURL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ServerUrl { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? Username { get; set; }

    [MaxLength(100)]
    public byte[]? PasswordEncrypted { get; set; }

    public byte EncryptionKey { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Identifier1 { get; set; }

    public string? VariableChar { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountExternalServices")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("ExternalServiceTypeId")]
    [InverseProperty("AccountExternalServices")]
    public virtual ExternalServiceType? ExternalServiceType { get; set; }

    [ForeignKey("ProtocolTypeId")]
    [InverseProperty("AccountExternalServices")]
    public virtual ProtocolType? ProtocolType { get; set; }
}
