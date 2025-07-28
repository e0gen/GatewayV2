using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
[Table("tblGlobalValues")]
public partial class TblGlobalValue
{
    public float? Prime { get; set; }

    [Column("PSPFeeMonthly", TypeName = "money")]
    public decimal? PspfeeMonthly { get; set; }

    [Column("PSPFeeTransaction", TypeName = "money")]
    public decimal? PspfeeTransaction { get; set; }

    [Column("PSPFeeVolumePercent")]
    public float? PspfeeVolumePercent { get; set; }

    public float? DebitTerminalVolumePercent { get; set; }

    public int? DeclineCountHours { get; set; }

    [Column(TypeName = "money")]
    public decimal? AnnualFeeAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? AnnualFeeLowRiskAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? AnnualFeeHighRiskAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? MonthlyFeeDollar { get; set; }

    [Column(TypeName = "money")]
    public decimal? MonthlyFeeEuro { get; set; }

    [Column(TypeName = "money")]
    public decimal? MonthlyFeeBankHigh { get; set; }

    [Column(TypeName = "money")]
    public decimal? MonthlyFeeBankLow { get; set; }

    [Column(TypeName = "money")]
    public decimal? AnnualFee3dSecureAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? AnnualFeeRegistrationAmount { get; set; }
}
