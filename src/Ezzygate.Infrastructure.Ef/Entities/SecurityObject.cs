using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SecurityObject", Schema = "System")]
[Index("AppModuleId", "Name", Name = "UIX_SecurityObject_AppModuleID_Name", IsUnique = true)]
public partial class SecurityObject
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Value { get; set; } = null!;

    [StringLength(100)]
    public string? Description { get; set; }

    [ForeignKey("AppModuleId")]
    [InverseProperty("SecurityObjects")]
    public virtual AppModule AppModule { get; set; } = null!;

    [InverseProperty("SecurityObject")]
    public virtual ICollection<SecurityObjectToAdminGroup> SecurityObjectToAdminGroups { get; set; } = new List<SecurityObjectToAdminGroup>();

    [InverseProperty("SecurityObject")]
    public virtual ICollection<SecurityObjectToLoginAccount> SecurityObjectToLoginAccounts { get; set; } = new List<SecurityObjectToLoginAccount>();
}
