using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("LphId", "LphRefId", "LphRefType")]
[Table("tblPasswordHistory")]
public partial class TblPasswordHistory
{
    [Key]
    [Column("LPH_ID")]
    public int LphId { get; set; }

    [Key]
    [Column("LPH_RefID")]
    public int LphRefId { get; set; }

    [Key]
    [Column("LPH_RefType")]
    public int LphRefType { get; set; }

    [Column("LPH_Insert", TypeName = "datetime")]
    public DateTime LphInsert { get; set; }

    [Column("LPH_IP")]
    [StringLength(20)]
    public string LphIp { get; set; } = null!;

    [Column("LPH_FailCount")]
    public int LphFailCount { get; set; }

    [Column("LPH_LastFail", TypeName = "datetime")]
    public DateTime LphLastFail { get; set; }

    [Column("LPH_Password256")]
    [MaxLength(200)]
    public byte[]? LphPassword256 { get; set; }

    [Column("LPH_LastSuccess", TypeName = "datetime")]
    public DateTime LphLastSuccess { get; set; }
}
