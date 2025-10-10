using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblEpaFileLog")]
public partial class TblEpaFileLog
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ActionDate { get; set; }

    public int ActionType { get; set; }

    [StringLength(100)]
    public string? StoredFileName { get; set; }

    [StringLength(100)]
    public string? OriginalFileName { get; set; }

    [StringLength(1500)]
    public string? Details { get; set; }

    [StringLength(100)]
    public string? Username { get; set; }

    [ForeignKey("ActionType")]
    [InverseProperty("TblEpaFileLogs")]
    public virtual TblEpaActionType ActionTypeNavigation { get; set; } = null!;
}
