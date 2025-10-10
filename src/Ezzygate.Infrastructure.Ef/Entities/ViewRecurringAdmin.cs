using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewRecurringAdmin
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("MerchantID")]
    public int MerchantId { get; set; }

    [StringLength(200)]
    public string MerchantName { get; set; } = null!;

    [Column("rs_ChargeCount")]
    public int RsChargeCount { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string? IntervalText { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CreateDate { get; set; }

    [StringLength(4000)]
    public string? Memo { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string TransType { get; set; } = null!;

    [Column("rs_Suspended")]
    public bool RsSuspended { get; set; }

    [Column("rs_Blocked")]
    public bool RsBlocked { get; set; }

    [Column("rs_Deleted")]
    public bool RsDeleted { get; set; }

    [Column("rs_Paid")]
    public bool RsPaid { get; set; }

    [Column("rs_Currency")]
    public int? RsCurrency { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyName { get; set; } = null!;

    [Column("rs_ChargeAmount", TypeName = "money")]
    public decimal RsChargeAmount { get; set; }

    [StringLength(14)]
    public string? Amount { get; set; }

    [Column("rs_ECheck")]
    public int? RsEcheck { get; set; }

    [Column("CCTypeID")]
    public short? CctypeId { get; set; }

    [Column("BINCountry")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Bincountry { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? CardNumber { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ChargeDateFirst { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ChargeDateLast { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ChargeDateNext { get; set; }

    public int? TransPassFirst { get; set; }
}
