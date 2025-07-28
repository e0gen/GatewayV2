using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AppModule", Schema = "System")]
[Index("Name", Name = "UIX_AppModule_Name", IsUnique = true)]
public partial class AppModule
{
    [Key]
    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Version { get; set; }

    [Precision(0)]
    public DateTime InstallDate { get; set; }

    public bool IsInstalled { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("AppModule")]
    public virtual ICollection<AppModuleAccountSetting> AppModuleAccountSettings { get; set; } = new List<AppModuleAccountSetting>();

    [InverseProperty("AppModule")]
    public virtual ICollection<AppModuleSetting> AppModuleSettings { get; set; } = new List<AppModuleSetting>();

    [InverseProperty("AppModule")]
    public virtual ICollection<SecurityObject> SecurityObjects { get; set; } = new List<SecurityObject>();
}
