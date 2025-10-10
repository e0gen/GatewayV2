using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRecurringSeries")]
[Index("RsCreditCard", Name = "IX_tblRecurringSeries_CreditCard", AllDescending = true)]
public partial class TblRecurringSeries
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("rs_Company")]
    public int? RsCompany { get; set; }

    [Column("rs_SeriesNumber")]
    public int RsSeriesNumber { get; set; }

    [Column("rs_ChargeCount")]
    public int RsChargeCount { get; set; }

    [Column("rs_IntervalUnit")]
    public int RsIntervalUnit { get; set; }

    [Column("rs_IntervalLength")]
    public int RsIntervalLength { get; set; }

    [Column("rs_StartDate", TypeName = "datetime")]
    public DateTime RsStartDate { get; set; }

    [Column("rs_CreditCard")]
    public int? RsCreditCard { get; set; }

    [Column("rs_ECheck")]
    public int? RsEcheck { get; set; }

    [Column("rs_Comments")]
    [StringLength(200)]
    public string RsComments { get; set; } = null!;

    [Column("rs_Suspended")]
    public bool RsSuspended { get; set; }

    [Column("rs_Blocked")]
    public bool RsBlocked { get; set; }

    [Column("rs_Paid")]
    public bool RsPaid { get; set; }

    [Column("rs_ChargeAmount", TypeName = "money")]
    public decimal RsChargeAmount { get; set; }

    [Column("rs_Currency")]
    public int? RsCurrency { get; set; }

    [Column("rs_Deleted")]
    public bool RsDeleted { get; set; }

    [Column("rs_Approval")]
    public bool RsApproval { get; set; }

    [Column("rs_Flexible")]
    public bool RsFlexible { get; set; }

    [Column("rs_SeriesTotal", TypeName = "money")]
    public decimal RsSeriesTotal { get; set; }

    [Column("rs_ReqTrmCode")]
    public int? RsReqTrmCode { get; set; }

    [Column("rs_IP")]
    [StringLength(50)]
    [Unicode(false)]
    public string RsIp { get; set; } = null!;

    [Column("rs_FirstApproval")]
    public bool? RsFirstApproval { get; set; }

    [Column("rs_Identifier")]
    [StringLength(50)]
    [Unicode(false)]
    public string? RsIdentifier { get; set; }

    [Column("rs_IsPrecreated")]
    public bool? RsIsPrecreated { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [ForeignKey("RsCompany")]
    [InverseProperty("TblRecurringSeries")]
    public virtual TblCompany? RsCompanyNavigation { get; set; }

    [ForeignKey("RsCreditCard")]
    [InverseProperty("TblRecurringSeries")]
    public virtual TblCreditCard? RsCreditCardNavigation { get; set; }

    [ForeignKey("RsCurrency")]
    [InverseProperty("TblRecurringSeries")]
    public virtual TblSystemCurrency? RsCurrencyNavigation { get; set; }

    [ForeignKey("RsEcheck")]
    [InverseProperty("TblRecurringSeries")]
    public virtual TblCheckDetail? RsEcheckNavigation { get; set; }

    [InverseProperty("RecurringSeriesNavigation")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("RecurringSeriesNavigation")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("RcSeriesNavigation")]
    public virtual ICollection<TblRecurringCharge> TblRecurringCharges { get; set; } = new List<TblRecurringCharge>();

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblRecurringSeries")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }
}
