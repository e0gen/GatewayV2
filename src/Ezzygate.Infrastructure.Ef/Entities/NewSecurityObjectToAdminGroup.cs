using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("SecurityObjectId", "AdminGroupId")]
[Table("new_SecurityObjectToAdminGroup", Schema = "System")]
public partial class NewSecurityObjectToAdminGroup
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Key]
    [Column("AdminGroup_id")]
    public short AdminGroupId { get; set; }

    public bool Value { get; set; }

    [ForeignKey("AdminGroupId")]
    [InverseProperty("NewSecurityObjectToAdminGroups")]
    public virtual AdminGroup AdminGroup { get; set; } = null!;

    [ForeignKey("SecurityObjectId")]
    [InverseProperty("NewSecurityObjectToAdminGroups")]
    public virtual NewSecurityObject SecurityObject { get; set; } = null!;
}
