using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransPass")]
[Index("CompanyId", "UnsettledInstallments", "Currency", "InsertDate", "Id", Name = "IX_tblCompanyTransPass_CompanyID_UnsettledInstallments_Currency_InsertDate_ID")]
[Index("CompanyId", "UnsettledInstallments", "IsTestOnly", Name = "IX_tblCompanyTransPass_CompanyID_UnsettledInstallments_isTestOnly")]
[Index("CreditType", Name = "IX_tblCompanyTransPass_CreditType")]
[Index("Currency", Name = "IX_tblCompanyTransPass_Currency")]
[Index("Currency", "CompanyId", "MerchantPd", "IsTestOnly", "UnsettledInstallments", Name = "IX_tblCompanyTransPass_Currency_CompanyID_MerchantPD_IsTestOnly_UnsettledInstallments", IsDescending = new[] { false, true, true, false, false })]
[Index("DebitCompanyId", Name = "IX_tblCompanyTransPass_DebitCompanyID")]
[Index("DeniedDate", Name = "IX_tblCompanyTransPass_DeniedDate", AllDescending = true)]
[Index("InsertDate", Name = "IX_tblCompanyTransPass_InsertDate", AllDescending = true)]
[Index("OriginalTransId", "DeniedStatus", "InsertDate", "CompanyId", "IsTestOnly", Name = "IX_tblCompanyTransPass_OriginalTransID_DeniedStatus_InsertDate_CompanyID_isTestOnly")]
[Index("PayId", Name = "IX_tblCompanyTransPass_PayID")]
[Index("PaymentMethod", Name = "IX_tblCompanyTransPass_PaymentMethod")]
[Index("PrimaryPayedId", Name = "IX_tblCompanyTransPass_PrimaryPayedID")]
[Index("TransPayerInfoId", Name = "IX_tblCompanyTransPass_TransPayerInfoID")]
[Index("UnsettledAmount", Name = "IX_tblCompanyTransPass_UnsettledAmount")]
[Index("TerminalNumber", Name = "IX_tblCompanyTransPass_terminalNumber")]
public partial class TblCompanyTransPass
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Column("DebitCompanyID")]
    public int? DebitCompanyId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("FraudDetectionLog_id")]
    public int FraudDetectionLogId { get; set; }

    [Column("OriginalTransID")]
    public int OriginalTransId { get; set; }

    [Column("PrimaryPayedID")]
    public int PrimaryPayedId { get; set; }

    [Column("PayID")]
    [StringLength(500)]
    public string PayId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public int? Currency { get; set; }

    public byte Payments { get; set; }

    public byte CreditType { get; set; }

    [Column("IPAddress")]
    [StringLength(50)]
    public string Ipaddress { get; set; } = null!;

    [Column("replyCode")]
    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    [StringLength(100)]
    public string OrderNumber { get; set; } = null!;

    public double Interest { get; set; }

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [StringLength(50)]
    public string ApprovalNumber { get; set; } = null!;

    [Precision(2)]
    public DateTime DeniedDate { get; set; }

    public byte DeniedStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DeniedPrintDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DeniedSendDate { get; set; }

    [StringLength(500)]
    public string DeniedAdminComment { get; set; } = null!;

    [Column("PD", TypeName = "smalldatetime")]
    public DateTime Pd { get; set; }

    [Column("MerchantPD", TypeName = "smalldatetime")]
    public DateTime MerchantPd { get; set; }

    [Column("PaymentMethod_id")]
    public byte PaymentMethodId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId1 { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    [Column("isTestOnly")]
    public bool IsTestOnly { get; set; }

    [Column("referringUrl")]
    [StringLength(500)]
    public string ReferringUrl { get; set; } = null!;

    [Column("payerIdUsed")]
    [StringLength(10)]
    public string PayerIdUsed { get; set; } = null!;

    [Column("netpayFee_transactionCharge", TypeName = "smallmoney")]
    public decimal EzzygateFeeTransactionCharge { get; set; }

    [Column("netpayFee_ratioCharge", TypeName = "smallmoney")]
    public decimal EzzygateFeeRatioCharge { get; set; }

    [StringLength(50)]
    public string? DebitReferenceCode { get; set; }

    [Column("OCurrency")]
    public byte? Ocurrency { get; set; }

    [Column("OAmount", TypeName = "money")]
    public decimal? Oamount { get; set; }

    [Column("netpayFee_chbCharge", TypeName = "money")]
    public decimal? EzzygateFeeChbCharge { get; set; }

    [Column("netpayFee_ClrfCharge", TypeName = "money")]
    public decimal? EzzygateFeeClrfCharge { get; set; }

    public short? PaymentMethod { get; set; }

    [Column(TypeName = "money")]
    public decimal? UnsettledAmount { get; set; }

    public int? UnsettledInstallments { get; set; }

    [Column("IPCountry")]
    [StringLength(2)]
    [Unicode(false)]
    public string Ipcountry { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal HandlingFee { get; set; }

    [Column("CTP_Status")]
    public byte CtpStatus { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal DebitFee { get; set; }

    [Column("BTFileName")]
    [StringLength(80)]
    public string? BtfileName { get; set; }

    public int? RecurringSeries { get; set; }

    public int? RecurringChargeNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeniedValDate { get; set; }

    [Column("MerchantRealPD", TypeName = "datetime")]
    public DateTime? MerchantRealPd { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? RefTrans { get; set; }

    public bool? IsChargeback { get; set; }

    [Column("PhoneDetailsID")]
    public int? PhoneDetailsId { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal? DebitFeeChb { get; set; }

    public bool? IsRetrievalRequest { get; set; }

    public bool? IsPendingChargeback { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    [Column("TransSource_id")]
    public byte? TransSourceId { get; set; }

    [Column("MobileDevice_id")]
    public int? MobileDeviceId { get; set; }

    [Column("AuthorizationBatchID")]
    public int? AuthorizationBatchId { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [StringLength(100)]
    public string? SystemText { get; set; }

    [StringLength(100)]
    public string? PayforText { get; set; }

    [Column("MerchantProduct_id")]
    public int? MerchantProductId { get; set; }

    [Column("CreditCardID")]
    public int? CreditCardId { get; set; }

    [Column("CheckDetailsID")]
    public int? CheckDetailsId { get; set; }

    [Column("PayerInfo_id")]
    public int? PayerInfoId { get; set; }

    [Column("TransPayerInfo_id")]
    public int? TransPayerInfoId { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [Column("Is3DSecure")]
    public bool? Is3Dsecure { get; set; }

    public bool? IsFraudByAcquirer { get; set; }

    public bool IsCashback { get; set; }

    public bool? IsCardPresent { get; set; }

    public short? PaymentMethodRef { get; set; }

    [ForeignKey("AuthorizationBatchId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual AuthorizationBatch? AuthorizationBatch { get; set; }

    [InverseProperty("TransPass")]
    public virtual ICollection<AuthorizationTransDatum> AuthorizationTransData { get; set; } = new List<AuthorizationTransDatum>();

    [InverseProperty("TransPass")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CheckDetailsId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblCheckDetail? CheckDetails { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblCompany Company { get; set; } = null!;

    [ForeignKey("CreditCardId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblCreditCard? CreditCard { get; set; }

    [ForeignKey("Currency")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblSystemCurrency? CurrencyNavigation { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [InverseProperty("TransPass")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("MobileDeviceId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual MobileDevice? MobileDevice { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    [ForeignKey("PhoneDetailsId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual PhoneDetail? PhoneDetails { get; set; }

    [ForeignKey("RecurringSeries")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TblRecurringSeries? RecurringSeriesNavigation { get; set; }

    [InverseProperty("TransPass")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("CaptureTransaction")]
    public virtual ICollection<TblAutoCapture> TblAutoCaptures { get; set; } = new List<TblAutoCapture>();

    [InverseProperty("TransAnswer")]
    public virtual TblCompanyTransApproval? TblCompanyTransApproval { get; set; }

    [InverseProperty("TransAns")]
    public virtual ICollection<TblCompanyTransInstallment> TblCompanyTransInstallments { get; set; } = new List<TblCompanyTransInstallment>();

    [InverseProperty("Trans")]
    public virtual ICollection<TblLogImportEpa> TblLogImportEpas { get; set; } = new List<TblLogImportEpa>();

    [InverseProperty("TransPass")]
    public virtual ICollection<TblLogPaymentPageTran> TblLogPaymentPageTrans { get; set; } = new List<TblLogPaymentPageTran>();

    [InverseProperty("TransPass")]
    public virtual ICollection<TblLogPendingFinalize> TblLogPendingFinalizes { get; set; } = new List<TblLogPendingFinalize>();

    [InverseProperty("RaTransPassNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [InverseProperty("Trans")]
    public virtual ICollection<TblRefundAsk> TblRefundAsks { get; set; } = new List<TblRefundAsk>();

    [InverseProperty("TransPass")]
    public virtual ICollection<TblRetrivalRequest> TblRetrivalRequests { get; set; } = new List<TblRetrivalRequest>();

    [InverseProperty("TransPass")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }

    [ForeignKey("TransSourceId")]
    [InverseProperty("TblCompanyTransPasses")]
    public virtual TransSource? TransSource { get; set; }

    [InverseProperty("TransPass")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();
}
