using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewEzzygate014
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string CompanyName { get; set; } = null!;

    [StringLength(80)]
    public string? CurrentStatus { get; set; }

    [StringLength(50)]
    public string? ServiceName { get; set; }

    public int? TransPassCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastTransPass { get; set; }
}
