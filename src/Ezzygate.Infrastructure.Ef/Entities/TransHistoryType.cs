using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransHistoryType", Schema = "List")]
public partial class TransHistoryType
{
    [Key]
    [Column("TransHistoryType_id")]
    public byte TransHistoryTypeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [InverseProperty("TransHistoryType")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();
}
