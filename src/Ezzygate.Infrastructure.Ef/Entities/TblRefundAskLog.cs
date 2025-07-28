using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRefundAskLog")]
public partial class TblRefundAskLog
{
    [Key]
    [Column("refundAskLog_id")]
    public int RefundAskLogId { get; set; }

    [Column("refundAsk_id")]
    public int RefundAskId { get; set; }

    [Column("ral_description")]
    [StringLength(2500)]
    public string? RalDescription { get; set; }

    [Column("ral_date", TypeName = "smalldatetime")]
    public DateTime RalDate { get; set; }

    [Column("ral_user")]
    [StringLength(50)]
    public string RalUser { get; set; } = null!;
}
