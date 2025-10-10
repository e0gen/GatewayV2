using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransCreditType", Schema = "List")]
public partial class TransCreditType
{
    [Key]
    [Column("TransCreditType_id")]
    public byte TransCreditTypeId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? NameHeb { get; set; }

    [StringLength(50)]
    public string? NameEng { get; set; }

    public bool? IsShow { get; set; }

    public byte? ShowOrder { get; set; }
}
