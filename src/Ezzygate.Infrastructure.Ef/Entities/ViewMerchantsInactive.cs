using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewMerchantsInactive
{
    [Column("ID")]
    public int Id { get; set; }

    public byte ActiveStatus { get; set; }

    [Column("Merchant Number")]
    [StringLength(50)]
    public string MerchantNumber { get; set; } = null!;

    [Column("Company Name")]
    [StringLength(4000)]
    public string? CompanyName { get; set; }

    [Column("Current Status")]
    [StringLength(80)]
    public string? CurrentStatus { get; set; }

    [Column("Status Color")]
    [StringLength(74)]
    public string StatusColor { get; set; } = null!;

    [Column("Signup Date")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SignupDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastTransPass { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastTransPreAuth { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastTransPending { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastTransFail { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? LastSettlement { get; set; }

    [StringLength(62)]
    [Unicode(false)]
    public string? SelectCheckBox { get; set; }

    [StringLength(78)]
    [Unicode(false)]
    public string? HistoryLink { get; set; }

    public int IsInactive { get; set; }

    [StringLength(119)]
    [Unicode(false)]
    public string? DeleteButton { get; set; }
}
