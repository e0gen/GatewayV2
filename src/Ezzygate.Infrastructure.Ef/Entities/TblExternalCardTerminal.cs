using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblExternalCardTerminal")]
public partial class TblExternalCardTerminal
{
    [Key]
    [Column("ExternalCardTerminal_id")]
    public int ExternalCardTerminalId { get; set; }

    [Column("ExternalCardProvider_id")]
    public byte ExternalCardProviderId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? AuthUsername { get; set; }

    [MaxLength(200)]
    public byte[]? AuthPassword256 { get; set; }

    [StringLength(100)]
    public string? AuthVar1 { get; set; }

    [StringLength(100)]
    public string? AuthVar2 { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ExternalCardProviderId")]
    [InverseProperty("TblExternalCardTerminals")]
    public virtual ExternalCardProvider ExternalCardProvider { get; set; } = null!;

    [InverseProperty("ExternalCardTerminal")]
    public virtual ICollection<TblExternalCardCustomer> TblExternalCardCustomers { get; set; } = new List<TblExternalCardCustomer>();

    [InverseProperty("ExternalCardTerminal")]
    public virtual ICollection<TblExternalCardTerminalToMerchant> TblExternalCardTerminalToMerchants { get; set; } = new List<TblExternalCardTerminalToMerchant>();
}
