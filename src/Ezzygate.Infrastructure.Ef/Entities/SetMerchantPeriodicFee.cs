using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantPeriodicFee", Schema = "Setting")]
public partial class SetMerchantPeriodicFee
{
    [Key]
    [Column("SetMerchantPeriodicFee_id")]
    public int SetMerchantPeriodicFeeId { get; set; }

    [Column("PeriodicFeeType_id")]
    public byte PeriodicFeeTypeId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column(TypeName = "decimal(19, 3)")]
    public decimal Amount { get; set; }

    public bool IsActive { get; set; }

    public DateOnly DateNextCharge { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("SetMerchantPeriodicFees")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantPeriodicFees")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
