using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblTransactionAmount")]
[Index("InsertDate", Name = "IX_tblTransactionAmount_InsertDate")]
[Index("TypeId", Name = "IX_tblTransactionAmount_TypeID", AllDescending = true)]
public partial class TblTransactionAmount
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TransPassID")]
    public int? TransPassId { get; set; }

    [Column("TransApprovalID")]
    public int? TransApprovalId { get; set; }

    [Column("TransPendingID")]
    public int? TransPendingId { get; set; }

    [Column("TransFailID")]
    public int? TransFailId { get; set; }

    [Column("TypeID")]
    public int TypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    public int Currency { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SettlementDate { get; set; }

    [Column("SettlementID")]
    public int? SettlementId { get; set; }

    [Column(TypeName = "money")]
    public decimal? SettledAmount { get; set; }

    public int? SettledCurrency { get; set; }

    public int? Installment { get; set; }

    [Column("SettlementAmountID")]
    public int? SettlementAmountId { get; set; }

    [Column("MerchantID")]
    public int? MerchantId { get; set; }

    [Column("DebitCompany_id")]
    public int? DebitCompanyId { get; set; }

    [Column("Affiliates_id")]
    public int? AffiliatesId { get; set; }

    [Column(TypeName = "decimal(6, 4)")]
    public decimal? PercentValue { get; set; }

    [ForeignKey("SettlementAmountId")]
    [InverseProperty("TblTransactionAmounts")]
    public virtual TblSettlementAmount? SettlementAmount { get; set; }
}
