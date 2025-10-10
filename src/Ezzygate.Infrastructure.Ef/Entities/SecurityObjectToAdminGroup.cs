using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("SecurityObjectId", "AdminGroupId")]
[Table("SecurityObjectToAdminGroup", Schema = "System")]
public partial class SecurityObjectToAdminGroup
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Key]
    [Column("AdminGroup_id")]
    public short AdminGroupId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Value { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Location { get; set; }

    [ForeignKey("AdminGroupId")]
    [InverseProperty("SecurityObjectToAdminGroups")]
    public virtual AdminGroup AdminGroup { get; set; } = null!;

    [ForeignKey("SecurityObjectId")]
    [InverseProperty("SecurityObjectToAdminGroups")]
    public virtual SecurityObject SecurityObject { get; set; } = null!;
}
