using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("DBVersion", Schema = "System")]
public partial class Dbversion
{
    [Key]
    [Column("DBVersion_id")]
    public int DbversionId { get; set; }

    [Precision(0)]
    public DateTime DeployDate { get; set; }

    [Column(TypeName = "decimal(6, 2)")]
    public decimal Version { get; set; }

    [StringLength(100)]
    public string? Comment { get; set; }
}
