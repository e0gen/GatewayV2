using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblWhiteListBIN")]
public partial class TblWhiteListBin
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("BIN")]
    [StringLength(20)]
    [Unicode(false)]
    public string Bin { get; set; } = null!;

    [Column("PrimaryID")]
    public int PrimaryId { get; set; }

    public int LastTransPass { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastTransPassDate { get; set; }
}
