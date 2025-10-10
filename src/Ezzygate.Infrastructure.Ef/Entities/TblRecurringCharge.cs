using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRecurringCharge")]
[Index("RcCreditCard", Name = "IX_tblRecurringCharge_CreditCard", AllDescending = true)]
[Index("RcSeries", "RcChargeNumber", Name = "ix_RecurringCharge_Series_ChargeNumber", IsUnique = true)]
public partial class TblRecurringCharge
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("rc_Series")]
    public int RcSeries { get; set; }

    [Column("rc_ChargeNumber")]
    public int RcChargeNumber { get; set; }

    [Column("rc_Date", TypeName = "datetime")]
    public DateTime RcDate { get; set; }

    [Column("rc_CreditCard")]
    public int? RcCreditCard { get; set; }

    [Column("rc_ECheck")]
    public int? RcEcheck { get; set; }

    [Column("rc_Comments")]
    [StringLength(200)]
    public string RcComments { get; set; } = null!;

    [Column("rc_Suspended")]
    public bool RcSuspended { get; set; }

    [Column("rc_Blocked")]
    public bool RcBlocked { get; set; }

    [Column("rc_Paid")]
    public bool RcPaid { get; set; }

    [Column("rc_Amount", TypeName = "money")]
    public decimal RcAmount { get; set; }

    [Column("rc_Currency")]
    public int? RcCurrency { get; set; }

    [Column("rc_Attempts")]
    public int RcAttempts { get; set; }

    [Column("rc_Pending")]
    public bool RcPending { get; set; }

    [Column("rc_SeriesTemp")]
    public int? RcSeriesTemp { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [ForeignKey("RcCreditCard")]
    [InverseProperty("TblRecurringCharges")]
    public virtual TblCreditCard? RcCreditCardNavigation { get; set; }

    [ForeignKey("RcCurrency")]
    [InverseProperty("TblRecurringCharges")]
    public virtual TblSystemCurrency? RcCurrencyNavigation { get; set; }

    [ForeignKey("RcEcheck")]
    [InverseProperty("TblRecurringCharges")]
    public virtual TblCheckDetail? RcEcheckNavigation { get; set; }

    [ForeignKey("RcSeries")]
    [InverseProperty("TblRecurringCharges")]
    public virtual TblRecurringSeries RcSeriesNavigation { get; set; } = null!;

    [InverseProperty("RaChargeNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblRecurringCharges")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }
}
