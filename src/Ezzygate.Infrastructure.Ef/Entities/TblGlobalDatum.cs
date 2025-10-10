using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("GdId", "GdGroup", "GdLng")]
[Table("tblGlobalData")]
[Index("GdGroup", Name = "IX_tblGlobalData_GD_Group")]
public partial class TblGlobalDatum
{
    [Key]
    [Column("GD_ID")]
    public int GdId { get; set; }

    [Key]
    [Column("GD_Group")]
    public int GdGroup { get; set; }

    [Key]
    [Column("GD_LNG")]
    public byte GdLng { get; set; }

    [Column("GD_Text")]
    [StringLength(80)]
    public string GdText { get; set; } = null!;

    [Column("GD_Description")]
    [StringLength(255)]
    public string? GdDescription { get; set; }

    [Column("GD_Color")]
    [StringLength(20)]
    public string? GdColor { get; set; }

    [Column("GD_Country")]
    [StringLength(20)]
    public string? GdCountry { get; set; }

    [Column("GD_Flags")]
    public short GdFlags { get; set; }

    [Column("GD_Currency")]
    [StringLength(80)]
    public string? GdCurrency { get; set; }
}
