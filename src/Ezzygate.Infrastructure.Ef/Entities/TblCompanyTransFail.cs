using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransFail")]
[Index("DebitReferenceCode", Name = "IX_tblCompanyTransFail_DebitReferenceCode")]
[Index("InsertDate", "CompanyId", "Currency", Name = "IX_tblCompanyTransFail_InsertDate_CompanyID_Currency")]
[Index("TransPayerInfoId", Name = "IX_tblCompanyTransFail_TransPayerInfoID")]
[Index("TerminalNumber", Name = "IX_tblCompanyTransFail_terminalNumber")]
public partial class TblCompanyTransFail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("FraudDetectionLog_id")]
    public int FraudDetectionLogId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("IPAddress")]
    [StringLength(50)]
    public string Ipaddress { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public byte Payments { get; set; }

    public byte CreditType { get; set; }

    [Column("replyCode")]
    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    [StringLength(100)]
    public string OrderNumber { get; set; } = null!;

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    public int TransType { get; set; }

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

    [StringLength(50)]
    public string? DebitReferenceCode { get; set; }

    [Column("netpayFee_transactionCharge", TypeName = "smallmoney")]
    public decimal EzzygateFeeTransactionCharge { get; set; }

    [Column("PayID")]
    public int? PayId { get; set; }

    [Column("IPCountry")]
    [StringLength(2)]
    [Unicode(false)]
    public string Ipcountry { get; set; } = null!;

    [Column("ctf_JumpIndex")]
    public byte CtfJumpIndex { get; set; }

    public int? AutoRefundStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AutoRefundDate { get; set; }

    [StringLength(20)]
    public string? AutoRefundReply { get; set; }

    public short DeclineCount { get; set; }

    public short DeclineSourceCount { get; set; }

    public short DeclineReplyCount { get; set; }

    public bool? IsGateway { get; set; }

    public short? PaymentMethod { get; set; }

    public int? Currency { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column("DebitCompanyID")]
    public int? DebitCompanyId { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal DebitFee { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }

    [Column("PhoneDetailsID")]
    public int? PhoneDetailsId { get; set; }

    [Column("TransSource_id")]
    public byte? TransSourceId { get; set; }

    [Column("MobileDevice_id")]
    public int? MobileDeviceId { get; set; }

    [StringLength(500)]
    public string? DebitDeclineReason { get; set; }

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

    public bool? IsCardPresent { get; set; }

    [StringLength(50)]
    public string ApprovalNumber { get; set; } = null!;

    [InverseProperty("TransFail")]
    public virtual ICollection<CartFailLog> CartFailLogs { get; set; } = new List<CartFailLog>();

    [ForeignKey("CheckDetailsId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TblCheckDetail? CheckDetails { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("CreditCardId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TblCreditCard? CreditCard { get; set; }

    [ForeignKey("Currency")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TblSystemCurrency? CurrencyNavigation { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [InverseProperty("TransFail")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("MobileDeviceId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual MobileDevice? MobileDevice { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    [ForeignKey("PhoneDetailsId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual PhoneDetail? PhoneDetails { get; set; }

    [InverseProperty("TransFail")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("DeclineTransaction")]
    public virtual ICollection<TblAutoCapture> TblAutoCaptures { get; set; } = new List<TblAutoCapture>();

    [InverseProperty("TransFail")]
    public virtual ICollection<TblLogPaymentPageTran> TblLogPaymentPageTrans { get; set; } = new List<TblLogPaymentPageTran>();

    [InverseProperty("TransFail")]
    public virtual ICollection<TblLogPendingFinalize> TblLogPendingFinalizes { get; set; } = new List<TblLogPendingFinalize>();

    [InverseProperty("RaTransFailNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [InverseProperty("TransFail")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }

    [ForeignKey("TransSourceId")]
    [InverseProperty("TblCompanyTransFails")]
    public virtual TransSource? TransSource { get; set; }

    [InverseProperty("TransFail")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();
}
