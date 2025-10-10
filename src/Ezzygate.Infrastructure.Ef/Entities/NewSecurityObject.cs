using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("new_SecurityObject", Schema = "System")]
public partial class NewSecurityObject
{
    [Key]
    [Column("SecurityObject_id")]
    public int SecurityObjectId { get; set; }

    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ObjectName { get; set; }

    [StringLength(100)]
    public string? Description { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string Permission { get; set; } = null!;

    [Column("SolutionList_id")]
    public byte? SolutionListId { get; set; }

    [ForeignKey("AppModuleId")]
    [InverseProperty("NewSecurityObjects")]
    public virtual NewAppModule AppModule { get; set; } = null!;

    [InverseProperty("SecurityObject")]
    public virtual ICollection<NewSecurityObjectToAdminGroup> NewSecurityObjectToAdminGroups { get; set; } = new List<NewSecurityObjectToAdminGroup>();

    [InverseProperty("SecurityObject")]
    public virtual ICollection<NewSecurityObjectToLoginAccount> NewSecurityObjectToLoginAccounts { get; set; } = new List<NewSecurityObjectToLoginAccount>();

    [ForeignKey("SolutionListId")]
    [InverseProperty("NewSecurityObjects")]
    public virtual SolutionList? SolutionList { get; set; }
}
