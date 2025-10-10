using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("BlockLevel", Schema = "List")]
public partial class BlockLevel
{
    [Key]
    [Column("BlockLevel_id")]
    public byte BlockLevelId { get; set; }

    [StringLength(50)]
    public string? Name { get; set; }

    [InverseProperty("BlBlockLevelNavigation")]
    public virtual ICollection<TblBlcommon> TblBlcommons { get; set; } = new List<TblBlcommon>();

    [InverseProperty("FcblBlockLevelNavigation")]
    public virtual ICollection<TblFraudCcBlackList> TblFraudCcBlackLists { get; set; } = new List<TblFraudCcBlackList>();
}
