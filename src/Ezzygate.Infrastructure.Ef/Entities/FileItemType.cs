using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("FileItemType", Schema = "List")]
public partial class FileItemType
{
    [Key]
    [Column("FileItemType_id")]
    public byte FileItemTypeId { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [InverseProperty("FileItemType")]
    public virtual ICollection<AccountFile> AccountFiles { get; set; } = new List<AccountFile>();
}
