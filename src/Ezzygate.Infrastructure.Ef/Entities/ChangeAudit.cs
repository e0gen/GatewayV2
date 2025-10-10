using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ChangeAudit", Schema = "Log")]
public partial class ChangeAudit
{
    [Key]
    [Column("ChangeAudit_id")]
    public int ChangeAuditId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ActionType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ObjectLocation { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ObjectName { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? ValueOld { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? ValueNew { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InitiatedBy { get; set; }
}
