using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCreditCardType")]
public partial class TblCreditCardType
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(50)]
    public string NameEng { get; set; } = null!;

    [StringLength(50)]
    public string NameHeb { get; set; } = null!;

    [StringLength(40)]
    public string IconFileName { get; set; } = null!;

    [Column("isShow")]
    public bool IsShow { get; set; }

    [Column("DBName")]
    [StringLength(40)]
    public string Dbname { get; set; } = null!;
}
