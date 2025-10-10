using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblEpaActionType")]
public partial class TblEpaActionType
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string? Brief { get; set; }

    [InverseProperty("ActionTypeNavigation")]
    public virtual ICollection<TblChbFileLog> TblChbFileLogs { get; set; } = new List<TblChbFileLog>();

    [InverseProperty("ActionTypeNavigation")]
    public virtual ICollection<TblEpaFileLog> TblEpaFileLogs { get; set; } = new List<TblEpaFileLog>();
}
