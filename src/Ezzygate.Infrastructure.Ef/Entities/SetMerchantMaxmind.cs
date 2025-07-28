using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantMaxmind", Schema = "Setting")]
public partial class SetMerchantMaxmind
{
    [Key]
    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public bool? IsEnabled { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ScoreAllowed { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? RiskScoreAllowed { get; set; }

    public bool? IsAllowPublicEmail { get; set; }

    [Column("SkipIPList")]
    [StringLength(500)]
    [Unicode(false)]
    public string? SkipIplist { get; set; }

    public bool? IsSkipVirtualTerminal { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantMaxmind")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
