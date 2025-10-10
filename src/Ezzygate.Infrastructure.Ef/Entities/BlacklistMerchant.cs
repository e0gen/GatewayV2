using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("BlacklistMerchant", Schema = "Risk")]
public partial class BlacklistMerchant
{
    [Key]
    [Column("BlacklistMerchant_id")]
    public int BlacklistMerchantId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? InsertDate { get; set; }

    [StringLength(50)]
    public string? InsertedBy { get; set; }

    [StringLength(150)]
    public string? FirstName { get; set; }

    [StringLength(150)]
    public string? LastName { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(50)]
    public string? Phone { get; set; }

    [StringLength(50)]
    public string? Cellular { get; set; }

    [Column("IDNumber")]
    [StringLength(15)]
    public string? Idnumber { get; set; }

    [StringLength(200)]
    public string? CompanyName { get; set; }

    [StringLength(200)]
    public string? CompanyLegalName { get; set; }

    [StringLength(15)]
    public string? CompanyLegalNumber { get; set; }

    [StringLength(500)]
    public string? CompanyStreet { get; set; }

    [StringLength(100)]
    public string? CompanyCity { get; set; }

    [StringLength(50)]
    public string? CompanyPhone { get; set; }

    [StringLength(50)]
    public string? CompanyFax { get; set; }

    [StringLength(200)]
    public string? Mail { get; set; }

    [Column("URL")]
    [StringLength(500)]
    public string? Url { get; set; }

    [StringLength(80)]
    public string? MerchantSupportEmail { get; set; }

    [StringLength(20)]
    public string? MerchantSupportPhoneNum { get; set; }

    [StringLength(50)]
    public string? IpOnReg { get; set; }

    [StringLength(50)]
    public string? PaymentPayeeName { get; set; }

    [StringLength(80)]
    public string? PaymentBranch { get; set; }

    [StringLength(80)]
    public string? PaymentAccount { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? SearchLink { get; set; }

    [StringLength(117)]
    [Unicode(false)]
    public string? DeleteButton { get; set; }

    [StringLength(100)]
    public string? PaymentBank { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("BlacklistMerchants")]
    public virtual TblCompany? Merchant { get; set; }
}
