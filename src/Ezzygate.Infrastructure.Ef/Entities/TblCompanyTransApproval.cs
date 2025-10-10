using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransApproval")]
[Index("TerminalNumber", Name = "IX_tblCompanyTransApproval_terminalNumber")]
public partial class TblCompanyTransApproval
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

    [Column("PayID")]
    public int PayId { get; set; }

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

    public double Interest { get; set; }

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [Column("approvalNumber")]
    [StringLength(50)]
    public string ApprovalNumber { get; set; } = null!;

    [Column("PaymentMethod_id")]
    public byte PaymentMethodId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId1 { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    [Column("referringUrl")]
    [StringLength(500)]
    public string ReferringUrl { get; set; } = null!;

    [StringLength(50)]
    public string? DebitReferenceCode { get; set; }

    [Column("netpayFee_transactionCharge", TypeName = "smallmoney")]
    public decimal EzzygateFeeTransactionCharge { get; set; }

    public int? RecurringSeries { get; set; }

    public int? RecurringChargeNumber { get; set; }

    public short? PaymentMethod { get; set; }

    public int? Currency { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column("DebitCompanyID")]
    public int? DebitCompanyId { get; set; }

    [Column("TransAnswerID")]
    public int? TransAnswerId { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal? DebitFee { get; set; }

    public bool? IsGateway { get; set; }

    [Column("isTestOnly")]
    public bool IsTestOnly { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }

    [Column("TransSource_id")]
    public byte? TransSourceId { get; set; }

    [Column("MobileDevice_id")]
    public int? MobileDeviceId { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [StringLength(100)]
    public string? SystemText { get; set; }

    [StringLength(100)]
    public string? PayforText { get; set; }

    [Column("CreditCardID")]
    public int? CreditCardId { get; set; }

    [Column("CheckDetailsID")]
    public int? CheckDetailsId { get; set; }

    [Column("PhoneDetailsID")]
    public int? PhoneDetailsId { get; set; }

    [Column("MerchantProduct_id")]
    public int? MerchantProductId { get; set; }

    [Column("PayerInfo_id")]
    public int? PayerInfoId { get; set; }

    [Column("TransPayerInfo_id")]
    public int? TransPayerInfoId { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [Column("Is3DSecure")]
    public bool? Is3Dsecure { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AcquirerReferenceNum { get; set; }

    public bool? IsCardPresent { get; set; }

    [StringLength(1000)]
    public string? TextValue { get; set; }

    public byte AuthStatus { get; set; }

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<AuthorizationTransDatum> AuthorizationTransData { get; set; } = new List<AuthorizationTransDatum>();

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CheckDetailsId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblCheckDetail? CheckDetails { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("CreditCardId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblCreditCard? CreditCard { get; set; }

    [ForeignKey("Currency")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblSystemCurrency? CurrencyNavigation { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("MobileDeviceId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual MobileDevice? MobileDevice { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    [ForeignKey("PhoneDetailsId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual PhoneDetail? PhoneDetails { get; set; }

    [ForeignKey("RecurringSeries")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TblRecurringSeries? RecurringSeriesNavigation { get; set; }

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("AuthorizedTransaction")]
    public virtual TblAutoCapture? TblAutoCapture { get; set; }

    [InverseProperty("TransApproval")]
    public virtual ICollection<TblLogPendingFinalize> TblLogPendingFinalizes { get; set; } = new List<TblLogPendingFinalize>();

    [InverseProperty("RaTransApprovalNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [ForeignKey("TransAnswerId")]
    [InverseProperty("TblCompanyTransApproval")]
    public virtual TblCompanyTransPass? TransAnswer { get; set; }

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }

    [ForeignKey("TransSourceId")]
    [InverseProperty("TblCompanyTransApprovals")]
    public virtual TransSource? TransSource { get; set; }

    [InverseProperty("TransPreAuth")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();
}
