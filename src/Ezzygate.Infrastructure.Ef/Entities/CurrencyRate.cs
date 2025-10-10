using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CurrencyRates", Schema = "Data")]
public partial class CurrencyRate
{
    [Key]
    [Column("CurrencyRates_id")]
    public int CurrencyRatesId { get; set; }

    [Column("CurrencyRateType_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string CurrencyRateTypeId { get; set; } = null!;

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column(TypeName = "decimal(10, 4)")]
    public decimal? BaseRate { get; set; }

    [Column(TypeName = "decimal(6, 4)")]
    public decimal? ExchangeFeeInd { get; set; }

    [Precision(0)]
    public DateTime? RateRequestDate { get; set; }

    [Precision(0)]
    public DateTime? RateValueDate { get; set; }

    [Column("BaseCurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? BaseCurrencyIsocode { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("CurrencyRates")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("CurrencyRateTypeId")]
    [InverseProperty("CurrencyRates")]
    public virtual CurrencyRateType CurrencyRateType { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("CurrencyRates")]
    public virtual TblCompany? Merchant { get; set; }
}
