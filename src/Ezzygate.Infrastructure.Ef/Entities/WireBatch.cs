using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("WireBatch", Schema = "Finance")]
public partial class WireBatch
{
    [Key]
    [Column("WireBatch_id")]
    public int WireBatchId { get; set; }

    [Column("WireProvider_Id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? WireProviderId { get; set; }

    public short WireCount { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(20)]
    public string? InsertUserName { get; set; }

    public byte? BatchStatus { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal BatchAmount { get; set; }

    [Column("BatchCurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string BatchCurrencyIsocode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? ConfirmationFileName { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? ProviderReference1 { get; set; }

    [StringLength(200)]
    public string? SourceAccountText { get; set; }

    public bool IsSendRequire { get; set; }

    [ForeignKey("BatchCurrencyIsocode")]
    [InverseProperty("WireBatches")]
    public virtual CurrencyList BatchCurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("WireProviderId")]
    [InverseProperty("WireBatches")]
    public virtual WireProvider? WireProvider { get; set; }

    [InverseProperty("WireBatch")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();
}
