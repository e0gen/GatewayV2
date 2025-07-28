using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyTransInstallments")]
[Index("TransAnsId", "InsId", Name = "IX_tblCompanyTransInstallments_transAnsID_InsID", IsUnique = true, IsDescending = new[] { true, false })]
public partial class TblCompanyTransInstallment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("InsID")]
    public byte InsId { get; set; }

    [Column("payID")]
    public int PayId { get; set; }

    [Column("amount", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("comment")]
    [StringLength(10)]
    public string Comment { get; set; } = null!;

    [Column("MerchantPD", TypeName = "smalldatetime")]
    public DateTime MerchantPd { get; set; }

    [Column("MerchantRealPD", TypeName = "datetime")]
    public DateTime? MerchantRealPd { get; set; }

    [Column("CompanyID")]
    public int? CompanyId { get; set; }

    [Column("transAnsID")]
    public int? TransAnsId { get; set; }

    [Column("netpayFee_transactionCharge", TypeName = "smallmoney")]
    public decimal? EzzygateFeeTransactionCharge { get; set; }

    [Column("netpayFee_ratioCharge", TypeName = "smallmoney")]
    public decimal? EzzygateFeeRatioCharge { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransInstallments")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("TransAnsId")]
    [InverseProperty("TblCompanyTransInstallments")]
    public virtual TblCompanyTransPass? TransAns { get; set; }
}
