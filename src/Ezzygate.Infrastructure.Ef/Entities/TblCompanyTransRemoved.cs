using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransRemoved")]
[Index("TerminalNumber", Name = "IX_tblCompanyTransRemoved_terminalNumber")]
public partial class TblCompanyTransRemoved
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("TransactionTypeID")]
    public int TransactionTypeId { get; set; }

    [Column("DebitCompanyID")]
    public short DebitCompanyId { get; set; }

    [Column("CustomerID")]
    public int CustomerId { get; set; }

    [Column("FraudDetectionLog_id")]
    public int FraudDetectionLogId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PayDate { get; set; }

    public bool IsPay { get; set; }

    [Column("IPAddress")]
    [StringLength(50)]
    public string Ipaddress { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public short Currency { get; set; }

    public int Payments { get; set; }

    public int CreditType { get; set; }

    [Column("replyCode")]
    [StringLength(50)]
    public string ReplyCode { get; set; } = null!;

    [StringLength(50)]
    public string OrderNumber { get; set; } = null!;

    public double Interest { get; set; }

    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [StringLength(7)]
    public string ApprovalNumber { get; set; } = null!;

    [Column("OriginalTransID")]
    public int OriginalTransId { get; set; }

    [Column("IPAddressDel")]
    [StringLength(50)]
    public string IpaddressDel { get; set; } = null!;

    [Column("PaymentMethod_id")]
    public byte PaymentMethodId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId1 { get; set; }

    [StringLength(50)]
    public string PaymentMethodDisplay { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DeniedDate { get; set; }

    public byte DeniedStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateDel { get; set; }

    [Column("isTestOnly")]
    public bool IsTestOnly { get; set; }

    [Column("referringUrl")]
    [StringLength(500)]
    public string ReferringUrl { get; set; } = null!;

    [Column("TransSource_id")]
    public byte? TransSourceId { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    [StringLength(100)]
    public string? SystemText { get; set; }

    [StringLength(100)]
    public string? PayforText { get; set; }

    [Column("MerchantProduct_id")]
    public int? MerchantProductId { get; set; }

    [Column("PayerInfo_id")]
    public int? PayerInfoId { get; set; }

    [Column("TransPayerInfo_id")]
    public int? TransPayerInfoId { get; set; }

    [Column("TransPaymentMethod_id")]
    public int? TransPaymentMethodId { get; set; }

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransRemoveds")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentMethodId")]
    [InverseProperty("TblCompanyTransRemoveds")]
    public virtual TransPaymentMethod? TransPaymentMethod { get; set; }
}
