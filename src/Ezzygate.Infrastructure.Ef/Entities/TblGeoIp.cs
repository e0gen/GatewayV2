using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("GiStart", "GiEnd")]
[Table("tblGeoIP")]
[Index("GiStart", "GiEnd", Name = "IX_tblGeoIP_Start_End_IsoCode", IsUnique = true, IsDescending = new[] { true, false })]
[Index("GiDiff", Name = "tblGeoIP_GI_Diff")]
public partial class TblGeoIp
{
    [Key]
    [Column("GI_Start")]
    public long GiStart { get; set; }

    [Key]
    [Column("GI_End")]
    public long GiEnd { get; set; }

    [Column("GI_Diff")]
    public long GiDiff { get; set; }

    [Column("GI_IsoCode")]
    [StringLength(2)]
    public string? GiIsoCode { get; set; }
}
