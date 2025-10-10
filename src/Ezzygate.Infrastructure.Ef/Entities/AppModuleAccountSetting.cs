using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AppModuleAccountSetting", Schema = "System")]
[Index("AppModuleId", "ValueName", "AccountId", Name = "UIX_AppModuleAccountSetting_AppModuleID_ValueName_AccountID", IsUnique = true)]
public partial class AppModuleAccountSetting
{
    [Key]
    [Column("AppModuleAccountSetting_id")]
    public int AppModuleAccountSettingId { get; set; }

    [Column("AppModule_id")]
    public int AppModuleId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string ValueName { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string Value { get; set; } = null!;

    [Column("Account_id")]
    public int AccountId { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AppModuleAccountSettings")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AppModuleId")]
    [InverseProperty("AppModuleAccountSettings")]
    public virtual AppModule AppModule { get; set; } = null!;
}
