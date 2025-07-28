using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("WireAccount", Schema = "Finance")]
public partial class WireAccount
{
    [Key]
    [Column("WireAccount_id")]
    public short WireAccountId { get; set; }

    [Column("WireProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string WireProviderId { get; set; } = null!;

    [StringLength(50)]
    public string? AccountName { get; set; }

    [StringLength(20)]
    public string? AccountNumber { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [StringLength(50)]
    public string? AccountBranch { get; set; }

    [StringLength(10)]
    public string? AccountBankCode { get; set; }

    [StringLength(100)]
    public string? Variable1 { get; set; }

    [StringLength(100)]
    public string? Variable2 { get; set; }

    [InverseProperty("WireAccount")]
    public virtual ICollection<ApplicationIdentity> ApplicationIdentities { get; set; } = new List<ApplicationIdentity>();

    [ForeignKey("WireProviderId")]
    [InverseProperty("WireAccounts")]
    public virtual WireProvider WireProvider { get; set; } = null!;
}
