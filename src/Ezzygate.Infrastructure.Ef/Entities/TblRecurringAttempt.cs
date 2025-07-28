using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRecurringAttempt")]
[Index("RaTransApproval", Name = "IX_tblRecurringAttempt_TransApproval", AllDescending = true)]
[Index("RaTransFail", Name = "IX_tblRecurringAttempt_TransFail", AllDescending = true)]
[Index("RaTransPass", Name = "IX_tblRecurringAttempt_TransPass", AllDescending = true)]
[Index("RaTransPending", Name = "IX_tblRecurringAttempt_TransPending", AllDescending = true)]
public partial class TblRecurringAttempt
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ra_Charge")]
    public int? RaCharge { get; set; }

    [Column("ra_Date", TypeName = "datetime")]
    public DateTime RaDate { get; set; }

    [Column("ra_CreditCard")]
    public int? RaCreditCard { get; set; }

    [Column("ra_ECheck")]
    public int? RaEcheck { get; set; }

    [Column("ra_ReplyCode")]
    [StringLength(50)]
    public string RaReplyCode { get; set; } = null!;

    [Column("ra_TransFail")]
    public int? RaTransFail { get; set; }

    [Column("ra_TransPass")]
    public int? RaTransPass { get; set; }

    [Column("ra_TransPending")]
    public int? RaTransPending { get; set; }

    [Column("ra_Comments")]
    [StringLength(200)]
    public string RaComments { get; set; } = null!;

    [Column("ra_TransApproval")]
    public int? RaTransApproval { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [ForeignKey("RaCharge")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblRecurringCharge? RaChargeNavigation { get; set; }

    [ForeignKey("RaCreditCard")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCreditCard? RaCreditCardNavigation { get; set; }

    [ForeignKey("RaEcheck")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCheckDetail? RaEcheckNavigation { get; set; }

    [ForeignKey("RaTransApproval")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCompanyTransApproval? RaTransApprovalNavigation { get; set; }

    [ForeignKey("RaTransFail")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCompanyTransFail? RaTransFailNavigation { get; set; }

    [ForeignKey("RaTransPass")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCompanyTransPass? RaTransPassNavigation { get; set; }

    [ForeignKey("RaTransPending")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TblCompanyTransPending? RaTransPendingNavigation { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblRecurringAttempts")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }
}
