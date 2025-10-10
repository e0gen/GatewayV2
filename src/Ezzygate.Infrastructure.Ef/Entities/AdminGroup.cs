using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AdminGroup", Schema = "System")]
public partial class AdminGroup
{
    [Key]
    [Column("AdminGroup_id")]
    public short AdminGroupId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(30)]
    public string? Description { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("AdminGroup")]
    public virtual ICollection<NewSecurityObjectToAdminGroup> NewSecurityObjectToAdminGroups { get; set; } = new List<NewSecurityObjectToAdminGroup>();

    [InverseProperty("AdminGroup")]
    public virtual ICollection<SecurityObjectToAdminGroup> SecurityObjectToAdminGroups { get; set; } = new List<SecurityObjectToAdminGroup>();

    [ForeignKey("AdminGroupId")]
    [InverseProperty("AdminGroups")]
    public virtual ICollection<AdminUser> AdminUsers { get; set; } = new List<AdminUser>();
}
