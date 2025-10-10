using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblTransactionPay")]
[Index("Currency", Name = "IX_tblTransactionPay_Currency")]
public partial class TblTransactionPay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("CompanyID")]
    public int CompanyId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PayDate { get; set; }

    [Column("isChargeVAT")]
    public bool IsChargeVat { get; set; }

    public double ExchangeRate { get; set; }

    [Column("transTotal")]
    public double TransTotal { get; set; }

    [Column("transChargeTotal")]
    public double TransChargeTotal { get; set; }

    [Column("transPayTotal")]
    public double TransPayTotal { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal SecurityDeposit { get; set; }

    [Column("transRollingReserve", TypeName = "money")]
    public decimal TransRollingReserve { get; set; }

    [StringLength(250)]
    public string Comment { get; set; } = null!;

    [Column("BillingCompanys_id")]
    public int BillingCompanysId { get; set; }

    [StringLength(3)]
    public string BillingLanguageShow { get; set; } = null!;

    public byte BillingCurrencyShow { get; set; }

    [StringLength(100)]
    public string BillingCompanyName { get; set; } = null!;

    [StringLength(500)]
    public string BillingCompanyAddress { get; set; } = null!;

    [StringLength(100)]
    public string BillingCompanyNumber { get; set; } = null!;

    [StringLength(100)]
    public string BillingCompanyEmail { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime BillingCreateDate { get; set; }

    [Column("isBillingPrintOriginal")]
    public bool IsBillingPrintOriginal { get; set; }

    [StringLength(2000)]
    public string BillingToInfo { get; set; } = null!;

    public byte PayType { get; set; }

    [Column("invoiceType")]
    public short InvoiceType { get; set; }

    public short TerminalType { get; set; }

    public double PrimePercent { get; set; }

    [Column("isShow")]
    public bool IsShow { get; set; }

    [Column("InvoiceDocumentID")]
    public int InvoiceDocumentId { get; set; }

    public int InvoiceNumber { get; set; }

    [Column("tp_Note")]
    [StringLength(255)]
    public string? TpNote { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal PayPercent { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal VatAmount { get; set; }

    public int? TotalCaptureCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalCaptureAmount { get; set; }

    public int? TotalAdminCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalAdminAmount { get; set; }

    public int? TotalSystemCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalSystemAmount { get; set; }

    public int? TotalRefundCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalRefundAmount { get; set; }

    public int? TotalChbCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalChbAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeProcessCapture { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeClarification { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeFinancing { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeHandling { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeBank { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalRollingReserve { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalRollingRelease { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalDirectDeposit { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalFeeChb { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalAmountTrans { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalAmountFee { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalPayout { get; set; }

    public int? Currency { get; set; }

    public bool IsGradedFees { get; set; }

    public bool IsTotalsCached { get; set; }

    [Column("PaymentMethod_id")]
    public short? PaymentMethodId { get; set; }

    public int? TotalCashbackCount { get; set; }

    [Column(TypeName = "money")]
    public decimal? TotalCashbackAmount { get; set; }

    [ForeignKey("Currency")]
    [InverseProperty("TblTransactionPays")]
    public virtual TblSystemCurrency? CurrencyNavigation { get; set; }

    [InverseProperty("Settlement")]
    public virtual ICollection<TblWireMoney> TblWireMoneys { get; set; } = new List<TblWireMoney>();

    [InverseProperty("MerchantSettlement")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();
}
