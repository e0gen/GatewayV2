using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblWireMoneyLog")]
public partial class TblWireMoneyLog
{
    [Key]
    [Column("wireMoneyLog_id")]
    public int WireMoneyLogId { get; set; }

    [Column("WireMoney_id")]
    public int? WireMoneyId { get; set; }

    [Column("wml_description")]
    [StringLength(250)]
    public string WmlDescription { get; set; } = null!;

    [Column("wml_date", TypeName = "smalldatetime")]
    public DateTime WmlDate { get; set; }

    [Column("wml_user")]
    [StringLength(50)]
    public string WmlUser { get; set; } = null!;

    [Column("logMasavFile_id")]
    public int? LogMasavFileId { get; set; }

    [ForeignKey("LogMasavFileId")]
    [InverseProperty("TblWireMoneyLogs")]
    public virtual TblLogMasavFile? LogMasavFile { get; set; }

    [ForeignKey("WireMoneyId")]
    [InverseProperty("TblWireMoneyLogs")]
    public virtual TblWireMoney? WireMoney { get; set; }
}
