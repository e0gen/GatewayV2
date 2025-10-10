using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("FraudDetection", Schema = "Log")]
public partial class FraudDetection
{
    [Key]
    [Column("FraudDetection_id")]
    public int FraudDetectionId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    public DateTime InsertDate { get; set; }

    [StringLength(1500)]
    public string? SendingString { get; set; }

    [StringLength(1500)]
    public string? ReturnAnswer { get; set; }

    [StringLength(1500)]
    public string? ReturnExplanation { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ReturnScore { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? ReturnRiskScore { get; set; }

    [StringLength(2)]
    public string? ReturnBinCountry { get; set; }

    [StringLength(5)]
    public string? ReturnQueriesRemaining { get; set; }

    [StringLength(30)]
    public string? ReferenceCode { get; set; }

    [Column("allowedScore", TypeName = "decimal(5, 2)")]
    public decimal? AllowedScore { get; set; }

    [Column("replyCode")]
    [StringLength(50)]
    public string? ReplyCode { get; set; }

    public bool? IsTemperScore { get; set; }

    public bool? IsProceed { get; set; }

    [Column("transAmount", TypeName = "money")]
    public decimal? TransAmount { get; set; }

    [Column("transCurrency")]
    public byte? TransCurrency { get; set; }

    [StringLength(50)]
    public string? PaymentMethodDisplay { get; set; }

    [StringLength(2)]
    public string? BinCountry { get; set; }

    public bool? IsDuplicateAnswer { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? SendingIp { get; set; }

    [StringLength(20)]
    public string? SendingCity { get; set; }

    [StringLength(2)]
    public string? SendingRegion { get; set; }

    [StringLength(10)]
    public string? SendingPostal { get; set; }

    [StringLength(2)]
    public string? SendingCountry { get; set; }

    [StringLength(30)]
    public string? SendingDomain { get; set; }

    [StringLength(6)]
    [Unicode(false)]
    public string? SendingBin { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? AllowedRiskScore { get; set; }

    public bool? IsCountryWhitelisted { get; set; }
}
