using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewMerchantsIsracard
{
    [Column("dt_CompanyNum_isracard")]
    [StringLength(50)]
    public string DtCompanyNumIsracard { get; set; } = null!;

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [Column("companyID")]
    public int CompanyId { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; } = null!;

    [Column("GD_Text")]
    [StringLength(80)]
    public string GdText { get; set; } = null!;

    [StringLength(50)]
    public string? Name { get; set; }

    public int? Transactions { get; set; }

    public DateOnly? LastTrans { get; set; }
}
