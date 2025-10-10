using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblWireMoneyFile")]
public partial class TblWireMoneyFile
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("wmf_WireMoneyID")]
    public int WmfWireMoneyId { get; set; }

    [Column("wmf_FileName")]
    [StringLength(100)]
    public string WmfFileName { get; set; } = null!;

    [Column("wmf_Description")]
    [StringLength(350)]
    public string WmfDescription { get; set; } = null!;

    [Column("wmf_Date", TypeName = "datetime")]
    public DateTime WmfDate { get; set; }

    [Column("wmf_User")]
    [StringLength(40)]
    public string WmfUser { get; set; } = null!;

    [Column("wmf_ParseResult")]
    public byte WmfParseResult { get; set; }

    [Column("wmf_ParseLog")]
    [StringLength(200)]
    public string? WmfParseLog { get; set; }

    [ForeignKey("WmfWireMoneyId")]
    [InverseProperty("TblWireMoneyFiles")]
    public virtual TblWireMoney WmfWireMoney { get; set; } = null!;
}
