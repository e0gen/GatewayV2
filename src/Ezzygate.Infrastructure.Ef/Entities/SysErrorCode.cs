using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SysErrorCode", Schema = "System")]
public partial class SysErrorCode
{
    [Key]
    [Column("SysErrorCode_id")]
    public int SysErrorCodeId { get; set; }

    [StringLength(20)]
    public string GroupName { get; set; } = null!;

    [Column("LanguageISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string LanguageIsocode { get; set; } = null!;

    public int ErrorCode { get; set; }

    [StringLength(255)]
    public string ErrorMessage { get; set; } = null!;
}
