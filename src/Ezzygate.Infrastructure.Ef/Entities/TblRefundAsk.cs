using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblRefundAsk")]
[Index("RefundAskDate", Name = "askDate")]
public partial class TblRefundAsk
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transID")]
    public int TransId { get; set; }

    [Column(TypeName = "money")]
    public decimal RefundAskAmount { get; set; }

    public int RefundAskCurrency { get; set; }

    [StringLength(300)]
    public string RefundAskComment { get; set; } = null!;

    [StringLength(30)]
    public string RefundAskConfirmationNum { get; set; } = null!;

    [Column(TypeName = "smalldatetime")]
    public DateTime RefundAskDate { get; set; }

    public int RefundAskStatus { get; set; }

    [StringLength(4000)]
    public string RefundAskStatusHistory { get; set; } = null!;

    public int RefundFlag { get; set; }

    public byte RefundType { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblRefundAsks")]
    public virtual TblCompany Company { get; set; } = null!;

    [ForeignKey("RefundAskCurrency")]
    [InverseProperty("TblRefundAsks")]
    public virtual TblSystemCurrency RefundAskCurrencyNavigation { get; set; } = null!;

    [ForeignKey("TransId")]
    [InverseProperty("TblRefundAsks")]
    public virtual TblCompanyTransPass Trans { get; set; } = null!;
}
