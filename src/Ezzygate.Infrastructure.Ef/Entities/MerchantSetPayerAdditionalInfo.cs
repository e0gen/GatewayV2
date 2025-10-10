using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Merchant set extra fields for getting more info from client
/// </summary>
[Table("MerchantSetPayerAdditionalInfo", Schema = "Setting")]
public partial class MerchantSetPayerAdditionalInfo
{
    [Key]
    public int MerchantSetPaymentFields { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [StringLength(25)]
    public string FieldLabel { get; set; } = null!;

    [StringLength(100)]
    public string? FieldDescription { get; set; }

    public bool IsRequired { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("MerchantSetPayerAdditionalInfos")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
