using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantAffiliate", Schema = "Setting")]
public partial class SetMerchantAffiliate
{
    [Key]
    [Column("SetMerchantAffiliate_id")]
    public int SetMerchantAffiliateId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("Affiliate_id")]
    public int AffiliateId { get; set; }

    [Column("UserID")]
    [StringLength(50)]
    public string? UserId { get; set; }

    [StringLength(1000)]
    public string? ConfigurationValues { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? FeesReducedPercentage { get; set; }

    [Precision(2)]
    public DateTime? SyncDateTime { get; set; }

    [ForeignKey("AffiliateId")]
    [InverseProperty("SetMerchantAffiliates")]
    public virtual TblAffiliate Affiliate { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantAffiliates")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
