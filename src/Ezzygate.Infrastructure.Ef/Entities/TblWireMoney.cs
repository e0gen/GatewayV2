using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblWireMoney")]
[Index("WireType", Name = "IX_tblWireMoney_WireType")]
[Index("WireDate", Name = "wireDate")]
public partial class TblWireMoney
{
    [Key]
    [Column("WireMoney_id")]
    public int WireMoneyId { get; set; }

    [Column("Company_id")]
    public int CompanyId { get; set; }

    [Column("WireSourceTbl_id")]
    public int WireSourceTblId { get; set; }

    [Column("wireInsertDate", TypeName = "smalldatetime")]
    public DateTime WireInsertDate { get; set; }

    public byte WireType { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime WireDate { get; set; }

    [Column(TypeName = "money")]
    public decimal WireAmount { get; set; }

    public int WireCurrency { get; set; }

    public double WireExchangeRate { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime WireStatusDate { get; set; }

    [StringLength(150)]
    public string WireStatusUser { get; set; } = null!;

    public byte WireStatus { get; set; }

    [StringLength(1000)]
    public string WireComment { get; set; } = null!;

    public byte WireFlag { get; set; }

    [StringLength(30)]
    public string WireConfirmationNum { get; set; } = null!;

    [Column("wirePrintApprovalStatusDate", TypeName = "smalldatetime")]
    public DateTime WirePrintApprovalStatusDate { get; set; }

    [Column("wirePrintApprovalStatusUser")]
    [StringLength(150)]
    public string WirePrintApprovalStatusUser { get; set; } = null!;

    [Column("wirePrintApprovalStatus")]
    public bool WirePrintApprovalStatus { get; set; }

    [Column("wireCompanyName")]
    [StringLength(200)]
    public string WireCompanyName { get; set; } = null!;

    [Column("wireCompanyLegalName")]
    [StringLength(200)]
    public string WireCompanyLegalName { get; set; } = null!;

    [Column("wireIDnumber")]
    [StringLength(15)]
    public string WireIdnumber { get; set; } = null!;

    [Column("wireCompanyLegalNumber")]
    [StringLength(15)]
    public string WireCompanyLegalNumber { get; set; } = null!;

    [Column("wirePaymentMethod")]
    [StringLength(80)]
    public string WirePaymentMethod { get; set; } = null!;

    [Column("wirePaymentPayeeName")]
    [StringLength(200)]
    public string WirePaymentPayeeName { get; set; } = null!;

    [Column("wirePaymentBank")]
    public int? WirePaymentBank { get; set; }

    [Column("wirePaymentBranch")]
    [StringLength(80)]
    public string WirePaymentBranch { get; set; } = null!;

    [Column("wirePaymentAccount")]
    [StringLength(80)]
    public string WirePaymentAccount { get; set; } = null!;

    [Column("wirePaymentAbroadAccountName")]
    [StringLength(80)]
    public string WirePaymentAbroadAccountName { get; set; } = null!;

    [Column("wirePaymentAbroadAccountNumber")]
    [StringLength(80)]
    public string WirePaymentAbroadAccountNumber { get; set; } = null!;

    [Column("wirePaymentAbroadBankName")]
    [StringLength(80)]
    public string WirePaymentAbroadBankName { get; set; } = null!;

    [Column("wirePaymentAbroadBankAddress")]
    [StringLength(200)]
    public string WirePaymentAbroadBankAddress { get; set; } = null!;

    [Column("wirePaymentAbroadSwiftNumber")]
    [StringLength(80)]
    public string WirePaymentAbroadSwiftNumber { get; set; } = null!;

    [Column("wirePaymentAbroadIBAN")]
    [StringLength(80)]
    public string WirePaymentAbroadIban { get; set; } = null!;

    [Column("wirePaymentAbroadAccountName2")]
    [StringLength(80)]
    public string WirePaymentAbroadAccountName2 { get; set; } = null!;

    [Column("wirePaymentAbroadAccountNumber2")]
    [StringLength(80)]
    public string WirePaymentAbroadAccountNumber2 { get; set; } = null!;

    [Column("wirePaymentAbroadBankName2")]
    [StringLength(80)]
    public string WirePaymentAbroadBankName2 { get; set; } = null!;

    [Column("wirePaymentAbroadBankAddress2")]
    [StringLength(200)]
    public string WirePaymentAbroadBankAddress2 { get; set; } = null!;

    [Column("wirePaymentAbroadSwiftNumber2")]
    [StringLength(80)]
    public string WirePaymentAbroadSwiftNumber2 { get; set; } = null!;

    [Column("wirePaymentAbroadIBAN2")]
    [StringLength(80)]
    public string WirePaymentAbroadIban2 { get; set; } = null!;

    [Column("wirePaymentAbroadABA2")]
    [StringLength(80)]
    public string WirePaymentAbroadAba2 { get; set; } = null!;

    [Column("wirePaymentAbroadSortCode2")]
    [StringLength(80)]
    public string WirePaymentAbroadSortCode2 { get; set; } = null!;

    [Column("isShow")]
    public bool IsShow { get; set; }

    [Column("wirePaymentAbroadABA")]
    [StringLength(80)]
    public string WirePaymentAbroadAba { get; set; } = null!;

    [Column("wirePaymentAbroadSortCode")]
    [StringLength(80)]
    public string WirePaymentAbroadSortCode { get; set; } = null!;

    [StringLength(100)]
    public string WirePaymentAbroadBankAddressSecond { get; set; } = null!;

    [StringLength(30)]
    public string WirePaymentAbroadBankAddressCity { get; set; } = null!;

    [StringLength(20)]
    public string WirePaymentAbroadBankAddressState { get; set; } = null!;

    [StringLength(20)]
    public string WirePaymentAbroadBankAddressZip { get; set; } = null!;

    public int WirePaymentAbroadBankAddressCountry { get; set; }

    [StringLength(100)]
    public string WirePaymentAbroadBankAddressSecond2 { get; set; } = null!;

    [StringLength(30)]
    public string WirePaymentAbroadBankAddressCity2 { get; set; } = null!;

    [StringLength(20)]
    public string WirePaymentAbroadBankAddressState2 { get; set; } = null!;

    [StringLength(20)]
    public string WirePaymentAbroadBankAddressZip2 { get; set; } = null!;

    public int WirePaymentAbroadBankAddressCountry2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastLogDate { get; set; }

    public int WireProcessingCurrency { get; set; }

    [Column("wireFee", TypeName = "money")]
    public decimal WireFee { get; set; }

    [Column("wirePaymentAbroadSepaBic")]
    [StringLength(11)]
    [Unicode(false)]
    public string WirePaymentAbroadSepaBic { get; set; } = null!;

    [Column("wirePaymentAbroadSepaBic2")]
    [StringLength(11)]
    [Unicode(false)]
    public string WirePaymentAbroadSepaBic2 { get; set; } = null!;

    [Column("SettlementID")]
    public int? SettlementId { get; set; }

    [Column("PaymentOrderID")]
    public int? PaymentOrderId { get; set; }

    [Column("AffiliateID")]
    public int? AffiliateId { get; set; }

    [Column("AffiliatePaymentsID")]
    public int? AffiliatePaymentsId { get; set; }

    [Column("wireApproveLevel1")]
    public bool? WireApproveLevel1 { get; set; }

    [Column("wireApproveLevel2")]
    public bool? WireApproveLevel2 { get; set; }

    [StringLength(255)]
    public string? ConfirmationFileName { get; set; }

    [Column("AccountBankAccount_id")]
    public int? AccountBankAccountId { get; set; }

    [Column("AccountPayee_id")]
    public int? AccountPayeeId { get; set; }

    [StringLength(250)]
    public string? Note { get; set; }

    [ForeignKey("AffiliateId")]
    [InverseProperty("TblWireMoneys")]
    public virtual TblAffiliate? Affiliate { get; set; }

    [ForeignKey("AffiliatePaymentsId")]
    [InverseProperty("TblWireMoneys")]
    public virtual TblAffiliatePayment? AffiliatePayments { get; set; }

    [ForeignKey("PaymentOrderId")]
    [InverseProperty("TblWireMoneys")]
    public virtual TblCompanyMakePaymentsRequest? PaymentOrder { get; set; }

    [ForeignKey("SettlementId")]
    [InverseProperty("TblWireMoneys")]
    public virtual TblTransactionPay? Settlement { get; set; }

    [InverseProperty("WmfWireMoney")]
    public virtual ICollection<TblWireMoneyFile> TblWireMoneyFiles { get; set; } = new List<TblWireMoneyFile>();

    [InverseProperty("WireMoney")]
    public virtual ICollection<TblWireMoneyLog> TblWireMoneyLogs { get; set; } = new List<TblWireMoneyLog>();
}
