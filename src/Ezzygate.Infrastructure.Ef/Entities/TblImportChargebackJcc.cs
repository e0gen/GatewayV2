using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblImportChargebackJCC")]
public partial class TblImportChargebackJcc
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("icb_IsProcessed")]
    public bool IcbIsProcessed { get; set; }

    [Column("POS_BRAND_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string PosBrandCode { get; set; } = null!;

    [Column("MERCHANT_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string MerchantNumber { get; set; } = null!;

    [Column("OUTLET_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string OutletNumber { get; set; } = null!;

    [Column("CHARGING_STATUS")]
    [StringLength(30)]
    [Unicode(false)]
    public string ChargingStatus { get; set; } = null!;

    [Column("MICROFILM_REF_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string MicrofilmRefNumber { get; set; } = null!;

    [Column("CARD_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string CardNumber { get; set; } = null!;

    [Column("NETWORK_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string NetworkCode { get; set; } = null!;

    [Column("ACRONYM")]
    [StringLength(30)]
    [Unicode(false)]
    public string Acronym { get; set; } = null!;

    [Column("AMOUNT")]
    [StringLength(30)]
    [Unicode(false)]
    public string Amount { get; set; } = null!;

    [Column("CURRENCY_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [Column("SOURCE_AMOUNT")]
    [StringLength(30)]
    [Unicode(false)]
    public string SourceAmount { get; set; } = null!;

    [Column("SOURCE_CURRENCY_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string SourceCurrencyCode { get; set; } = null!;

    [Column("REASON_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ReasonCode { get; set; } = null!;

    [Column("TRANSACTION_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string TransactionDate { get; set; } = null!;

    [Column("PROCESSING_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ProcessingDate { get; set; } = null!;

    [Column("AUTHORIZATION_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string AuthorizationCode { get; set; } = null!;

    [Column("TRN_ID")]
    [StringLength(30)]
    [Unicode(false)]
    public string TrnId { get; set; } = null!;

    [Column("icb_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string IcbUser { get; set; } = null!;

    [Column("icb_FileName")]
    [StringLength(100)]
    [Unicode(false)]
    public string IcbFileName { get; set; } = null!;

    [Column("icb_ChargebackDate", TypeName = "datetime")]
    public DateTime IcbChargebackDate { get; set; }

    [Column("CHARGEBACK_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ChargebackDate { get; set; } = null!;
}
