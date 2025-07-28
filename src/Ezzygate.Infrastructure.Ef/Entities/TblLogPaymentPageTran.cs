using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblLogPaymentPageTrans")]
[Index("LpptLogPaymentPageId", Name = "IX_tblLogPaymentPageTrans_LogPaymentPageID")]
public partial class TblLogPaymentPageTran
{
    [Key]
    [Column("LogPaymentPageTrans_id")]
    public int LogPaymentPageTransId { get; set; }

    [Column("Lppt_Date")]
    public DateOnly LpptDate { get; set; }

    [Column("Lppt_LogPaymentPage_id")]
    public int LpptLogPaymentPageId { get; set; }

    [Column("Lppt_PaymentMethodType")]
    public short? LpptPaymentMethodType { get; set; }

    [Column("Lppt_RequestString")]
    [StringLength(4000)]
    public string? LpptRequestString { get; set; }

    [Column("Lppt_ResponseString")]
    [StringLength(4000)]
    public string? LpptResponseString { get; set; }

    [Column("Lppt_ReplyCode")]
    [StringLength(50)]
    public string? LpptReplyCode { get; set; }

    [Column("Lppt_ReplyDesc")]
    [StringLength(500)]
    public string? LpptReplyDesc { get; set; }

    [Column("Lppt_TransNum")]
    public int? LpptTransNum { get; set; }

    [Column("Lppt_DateTime")]
    [Precision(2)]
    public DateTime LpptDateTime { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [ForeignKey("LpptLogPaymentPageId")]
    [InverseProperty("TblLogPaymentPageTrans")]
    public virtual TblLogPaymentPage LpptLogPaymentPage { get; set; } = null!;

    [ForeignKey("TransFailId")]
    [InverseProperty("TblLogPaymentPageTrans")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("TblLogPaymentPageTrans")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("TblLogPaymentPageTrans")]
    public virtual TblCompanyTransPending? TransPending { get; set; }
}
