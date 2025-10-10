using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantSetCartInstallments", Schema = "Setting")]
public partial class MerchantSetCartInstallment
{
    [Key]
    public int MerchantSetCartInstallments { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column(TypeName = "decimal(9, 0)")]
    public decimal Amount { get; set; }

    public byte MaxInstallments { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("MerchantSetCartInstallments")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
