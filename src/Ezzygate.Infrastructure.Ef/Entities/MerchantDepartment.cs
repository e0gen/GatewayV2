using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantDepartment")]
public partial class MerchantDepartment
{
    [Key]
    [Column("MerchantDepartment_id")]
    public byte MerchantDepartmentId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("MerchantDepartment")]
    public virtual ICollection<TblCompany> TblCompanies { get; set; } = new List<TblCompany>();
}
