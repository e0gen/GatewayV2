using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("StatusHistory", Schema = "Log")]
public partial class StatusHistory
{
    [Key]
    [Column("StatusHistory_id")]
    public int StatusHistoryId { get; set; }

    [Column("StatusHistoryType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string StatusHistoryTypeId { get; set; } = null!;

    [Column("ActionStatus_id")]
    public byte ActionStatusId { get; set; }

    public int SourceIdentity { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(50)]
    public string? InsertUserName { get; set; }

    [Column("InsertIPAddress")]
    [StringLength(50)]
    public string? InsertIpaddress { get; set; }

    [StringLength(1000)]
    public string? VariableChar { get; set; }

    [ForeignKey("ActionStatusId")]
    [InverseProperty("StatusHistories")]
    public virtual ActionStatus ActionStatus { get; set; } = null!;

    [ForeignKey("StatusHistoryTypeId")]
    [InverseProperty("StatusHistories")]
    public virtual StatusHistoryType StatusHistoryType { get; set; } = null!;
}
