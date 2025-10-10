using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantInstallments", Schema = "Setting")]
[Index("MerchantId", "InstallmentNum", Name = "UIX_SetMerchantInstallments_MerchantID_InstallmentNum", IsUnique = true)]
public partial class SetMerchantInstallment
{
    [Key]
    [Column("SetMerchantInstallments_id")]
    public int SetMerchantInstallmentsId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public byte InstallmentNum { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal FeePercentAdd { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantInstallments")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
