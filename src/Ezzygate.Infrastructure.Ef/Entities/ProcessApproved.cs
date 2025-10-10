using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ProcessApproved", Schema = "Track")]
public partial class ProcessApproved
{
    [Key]
    [Column("ProcessApproved_id")]
    public int ProcessApprovedId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("PaymentMethod_id")]
    public short? PaymentMethodId { get; set; }

    [Column("TransCreditType_id")]
    public byte? TransCreditTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TransDate { get; set; }

    [Column(TypeName = "money")]
    public decimal? TransAmount { get; set; }

    public byte? TransCurrency { get; set; }

    public byte? TransInstallments { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TransIpAddress { get; set; }

    [MaxLength(40)]
    public byte[]? CreditCardNumber256 { get; set; }

    [MaxLength(40)]
    public byte[]? CheckingAccountNumber256 { get; set; }

    public int? PaymentMethodStamp { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("ProcessApproveds")]
    public virtual TblCompany? Merchant { get; set; }

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("ProcessApproveds")]
    public virtual PaymentMethod? PaymentMethod { get; set; }
}
