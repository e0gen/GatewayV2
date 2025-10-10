using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblMerchantGroup")]
public partial class TblMerchantGroup
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("mg_Name")]
    [StringLength(50)]
    public string? MgName { get; set; }

    [InverseProperty("MerchantGroup")]
    public virtual ICollection<ApplicationIdentityToMerchantGroup> ApplicationIdentityToMerchantGroups { get; set; } = new List<ApplicationIdentityToMerchantGroup>();

    [InverseProperty("Group")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();
}
