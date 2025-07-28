using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewRecurringUnprocessed
{
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

    [Column("rs_Company")]
    public int? RsCompany { get; set; }
}
