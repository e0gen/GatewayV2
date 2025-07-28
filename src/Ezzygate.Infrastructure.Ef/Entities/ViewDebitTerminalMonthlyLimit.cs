using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewDebitTerminalMonthlyLimit
{
    [Column("DebitCompanyID")]
    public byte DebitCompanyId { get; set; }

    [Column("TerminalID")]
    public int TerminalId { get; set; }

    [Column("dc_Name")]
    [StringLength(50)]
    public string DcName { get; set; } = null!;

    [Column("dt_Name")]
    [StringLength(80)]
    public string DtName { get; set; } = null!;

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    public int Limit { get; set; }

    [StringLength(100)]
    public string NotifyUsers { get; set; } = null!;

    [Column("NotifyUsersSMS")]
    [StringLength(100)]
    public string NotifyUsersSms { get; set; } = null!;

    public int Quantity { get; set; }

    public bool WasSent { get; set; }

    [Column("LimitMC")]
    public int LimitMc { get; set; }

    [Column("NotifyUsersMC")]
    [StringLength(100)]
    public string NotifyUsersMc { get; set; } = null!;

    [Column("NotifyUsersSMSMC")]
    [StringLength(100)]
    public string NotifyUsersSmsmc { get; set; } = null!;

    [Column("QuantityMC")]
    public int QuantityMc { get; set; }

    [Column("WasSentMC")]
    public bool WasSentMc { get; set; }
}
