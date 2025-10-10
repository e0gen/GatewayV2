using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblSecurityDocument")]
public partial class TblSecurityDocument
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("sd_URL")]
    [StringLength(100)]
    public string SdUrl { get; set; } = null!;

    [Column("sd_Title")]
    [StringLength(100)]
    public string SdTitle { get; set; } = null!;

    [Column("sd_IsManaged")]
    public bool SdIsManaged { get; set; }

    [Column("sd_IsLogged")]
    public bool SdIsLogged { get; set; }

    [InverseProperty("SdgDocumentNavigation")]
    public virtual ICollection<TblSecurityDocumentGroup> TblSecurityDocumentGroups { get; set; } = new List<TblSecurityDocumentGroup>();

    [InverseProperty("SlDocumentNavigation")]
    public virtual ICollection<TblSecurityLog> TblSecurityLogs { get; set; } = new List<TblSecurityLog>();
}
