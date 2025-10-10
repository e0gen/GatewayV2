using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("WireHistory", Schema = "Finance")]
public partial class WireHistory
{
    [Key]
    [Column("WireHistory_id")]
    public int WireHistoryId { get; set; }

    [Column("Wire_id")]
    public int WireId { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }

    [StringLength(50)]
    public string? UserName { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? FileName { get; set; }

    [ForeignKey("WireId")]
    [InverseProperty("WireHistories")]
    public virtual Wire Wire { get; set; } = null!;
}
