using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AuthorizationTransData", Schema = "Trans")]
public partial class AuthorizationTransDatum
{
    [Key]
    [Column("AuthorizationTransData_id")]
    public int AuthorizationTransDataId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [StringLength(4000)]
    public string VariableChar { get; set; } = null!;

    [ForeignKey("TransPassId")]
    [InverseProperty("AuthorizationTransData")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("AuthorizationTransData")]
    public virtual TblCompanyTransPending? TransPending { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("AuthorizationTransData")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }
}
