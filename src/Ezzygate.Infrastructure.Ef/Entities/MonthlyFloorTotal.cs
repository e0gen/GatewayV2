using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MonthlyFloorTotal", Schema = "Track")]
public partial class MonthlyFloorTotal
{
    [Key]
    [Column("MonthlyFloorTotal_id")]
    public int MonthlyFloorTotalId { get; set; }

    [Column("SetTransactionFloor_id")]
    public int SetTransactionFloorId { get; set; }

    [Precision(2)]
    public DateTime? DateInFocus { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Amount { get; set; }

    [ForeignKey("SetTransactionFloorId")]
    [InverseProperty("MonthlyFloorTotals")]
    public virtual SetTransactionFloor SetTransactionFloor { get; set; } = null!;
}
