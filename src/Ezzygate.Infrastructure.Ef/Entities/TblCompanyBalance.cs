using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyBalance")]
[Index("CompanyId", "Currency", Name = "IX_tblCompanyBalance_company_id_currency")]
public partial class TblCompanyBalance
{
    [Key]
    [Column("companyBalance_id")]
    public int CompanyBalanceId { get; set; }

    [Column("sourceTbl_id")]
    public int SourceTblId { get; set; }

    [Column("sourceType")]
    public byte SourceType { get; set; }

    [Column("sourceInfo")]
    [StringLength(100)]
    public string SourceInfo { get; set; } = null!;

    [Column("insertDate", TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("amount", TypeName = "money")]
    public decimal Amount { get; set; }

    [Column("status")]
    public byte Status { get; set; }

    [Column("comment")]
    [StringLength(200)]
    public string Comment { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal BalanceExpected { get; set; }

    [Column(TypeName = "money")]
    public decimal BalanceCurrent { get; set; }

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("currency")]
    public int? Currency { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyBalances")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("Currency")]
    [InverseProperty("TblCompanyBalances")]
    public virtual TblSystemCurrency? CurrencyNavigation { get; set; }
}
