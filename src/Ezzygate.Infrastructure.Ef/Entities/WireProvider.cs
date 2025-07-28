using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("WireProvider", Schema = "List")]
public partial class WireProvider
{
    [Key]
    [Column("WireProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string WireProviderId { get; set; } = null!;

    [StringLength(30)]
    public string Name { get; set; } = null!;

    public int? NextWireBatchNum { get; set; }

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

    [InverseProperty("PreferredWireProvider")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("WireProvider")]
    public virtual ICollection<WireAccount> WireAccounts { get; set; } = new List<WireAccount>();

    [InverseProperty("WireProvider")]
    public virtual ICollection<WireBatch> WireBatches { get; set; } = new List<WireBatch>();

    [InverseProperty("WireProvider")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();
}
