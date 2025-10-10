using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitCompanyPaymentTokenization")]
public partial class TblDebitCompanyPaymentTokenization
{
    [Key]
    [Column("DebitCompanyPaymentTokenization_id")]
    public int DebitCompanyPaymentTokenizationId { get; set; }

    [Column("DebitCompany_id")]
    public int DebitCompanyId { get; set; }

    [StringLength(128)]
    public string PaymentMethodHash { get; set; } = null!;

    [StringLength(128)]
    public string Token { get; set; } = null!;

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TblDebitCompanyPaymentTokenizations")]
    public virtual TblDebitCompany DebitCompany { get; set; } = null!;
}
