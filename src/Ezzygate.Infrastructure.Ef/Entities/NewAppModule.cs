using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("new_AppModule", Schema = "System")]
[Index("Name", Name = "UIX_new_AppModule_Name", IsUnique = true)]
public partial class NewAppModule
{
    [Key]
    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Precision(0)]
    public DateTime InstallDate { get; set; }

    public bool IsInstalled { get; set; }

    public bool IsActive { get; set; }

    public long FileVersion { get; set; }

    [InverseProperty("AppModule")]
    public virtual ICollection<NewSecurityObject> NewSecurityObjects { get; set; } = new List<NewSecurityObject>();
}
