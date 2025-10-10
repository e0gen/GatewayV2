using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AppModuleSetting", Schema = "System")]
[Index("AppModuleId", "ValueName", Name = "UIX_AppModuleSetting_AppModuleID_ValueName", IsUnique = true)]
public partial class AppModuleSetting
{
    [Key]
    [Column("AppModuleSetting_id")]
    public int AppModuleSettingId { get; set; }

    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ValueName { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Value { get; set; } = null!;

    [ForeignKey("AppModuleId")]
    [InverseProperty("AppModuleSettings")]
    public virtual AppModule AppModule { get; set; } = null!;
}
