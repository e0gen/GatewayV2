using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAutoCapture")]
[Index("ActualDate", Name = "IX_tblAutoCapture_ActualDate", AllDescending = true)]
[Index("CaptureTransactionId", Name = "IX_tblAutoCapture_CaptureTransactionID", AllDescending = true)]
[Index("DeclineTransactionId", Name = "IX_tblAutoCapture_DeclineTransactionID", AllDescending = true)]
[Index("ScheduledDate", Name = "IX_tblAutoCapture_ScheduledDate", AllDescending = true)]
public partial class TblAutoCapture
{
    [Key]
    [Column("AuthorizedTransactionID")]
    public int AuthorizedTransactionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ScheduledDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ActualDate { get; set; }

    [Column("CaptureTransactionID")]
    public int? CaptureTransactionId { get; set; }

    [Column("DeclineTransactionID")]
    public int? DeclineTransactionId { get; set; }

    [ForeignKey("AuthorizedTransactionId")]
    [InverseProperty("TblAutoCapture")]
    public virtual TblCompanyTransApproval AuthorizedTransaction { get; set; } = null!;

    [ForeignKey("CaptureTransactionId")]
    [InverseProperty("TblAutoCaptures")]
    public virtual TblCompanyTransPass? CaptureTransaction { get; set; }

    [ForeignKey("DeclineTransactionId")]
    [InverseProperty("TblAutoCaptures")]
    public virtual TblCompanyTransFail? DeclineTransaction { get; set; }
}
