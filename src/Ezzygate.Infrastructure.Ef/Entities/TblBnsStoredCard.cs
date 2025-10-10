using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBnsStoredCard")]
public partial class TblBnsStoredCard
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [MaxLength(200)]
    public byte[] Identifier256 { get; set; } = null!;
}
