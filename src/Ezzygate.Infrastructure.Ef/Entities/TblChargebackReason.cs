using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("Brand", "ReasonCode")]
[Table("tblChargebackReason")]
public partial class TblChargebackReason
{
    [Key]
    [StringLength(20)]
    public string Brand { get; set; } = null!;

    [Key]
    public int ReasonCode { get; set; }

    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(3000)]
    public string? Description { get; set; }

    [StringLength(3000)]
    public string? MediaRequired { get; set; }

    public bool IsPendingChargeback { get; set; }

    [StringLength(512)]
    public string? RefundInfo { get; set; }
}
