using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransPending")]
[Index("InsertDate", Name = "IX_tblCompanyTransPending_InsertDate")]
public partial class TblCompanyTransPending
{
    [Key]
    [Column("companyTransPending_id")]
    public int CompanyTransPendingId { get; set; }

    [Column("companyBatchFiles_id")]
    public int CompanyBatchFilesId { get; set; }

    [Column("transactionSource_id")]
    public int TransactionSourceId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("FraudDetectionLog_id")]
    public int FraudDetectionLogId { get; set; }

    [Column("insertDate", TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    [Column("IPAddress")]
    [StringLength(50)]
    public string IpAddress { get; set; } = null!;

    [Column("replyCode")]
    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    [Column("trans_amount", TypeName = "money")]
    public decimal TransAmount { get; set; }

    [Column("trans_creditType")]
    public byte TransCreditType { get; set; }

    [Column("trans_payments")]
    public byte TransPayments { get; set; }

    [Column("trans_originalID")]
    public int TransOriginalId { get; set; }

    [Column("trans_order")]
    [StringLength(100)]
    public string TransOrder { get; set; } = null!;

    [StringLength(40)]
    public string DebitReferenceCode { get; set; } = null!;

    [StringLength(50)]
    public string DebitApprovalNumber { get; set; } = null!;

    public byte Locked { get; set; }

    [Column("ID")]
    public int Id { get; set; }

    [Column("payerIdUsed")]
    [StringLength(10)]
    public string PayerIdUsed { get; set; } = null!;

    [StringLength(100)]
    public string OrderNumber { get; set; } = null!;

    public short? PaymentMethod { get; set; }

    [Column("trans_currency")]
    public int? TransCurrency { get; set; }

    public int? Currency { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("CompanyID")]
    public int? CompanyId1 { get; set; }

    [Column("DebitCompanyID")]
    public int? DebitCompanyId { get; set; }

    [Column("IDNew")]
    public int? Idnew { get; set; }

    [Column("isTestOnly")]
    public bool IsTestOnly { get; set; }

    [Column("trans_type")]
    public int TransType { get; set; }

    [StringLength(50)]
    public string? DebitReferenceNum { get; set; }

    [Column("OriginalTransID")]
    public int? OriginalTransId { get; set; }

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

    [Column("OCurrency")]
    public byte? Ocurrency { get; set; }

    [Column("OAmount", TypeName = "money")]
    public decimal? Oamount { get; set; }

    [Column("CCStorageID")]
    public int? CcstorageId { get; set; }

    [StringLength(1000)]
    public string? TextValue { get; set; }

    [InverseProperty("TransPending")]
    public virtual ICollection<AuthorizationTransDatum> AuthorizationTransData { get; set; } = new List<AuthorizationTransDatum>();

    [InverseProperty("TransPending")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CheckDetailsId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TblCheckDetail? CheckDetails { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("CreditCardId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TblCreditCard? CreditCard { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [InverseProperty("TransPending")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("MobileDeviceId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual MobileDevice? MobileDevice { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    [ForeignKey("PhoneDetailsId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual PhoneDetail? PhoneDetails { get; set; }

    [InverseProperty("TransPending")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("TransPending")]
    public virtual ICollection<TblLogPaymentPageTran> TblLogPaymentPageTrans { get; set; } = new List<TblLogPaymentPageTran>();

    [InverseProperty("RaTransPendingNavigation")]
    public virtual ICollection<TblRecurringAttempt> TblRecurringAttempts { get; set; } = new List<TblRecurringAttempt>();

    [ForeignKey("TransCurrency")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TblSystemCurrency? TransCurrencyNavigation { get; set; }

    [InverseProperty("TransPending")]
    public virtual ICollection<TransHistory> TransHistories { get; set; } = new List<TransHistory>();

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }

    [ForeignKey("TransSourceId")]
    [InverseProperty("TblCompanyTransPendings")]
    public virtual TransSource? TransSource { get; set; }
}
