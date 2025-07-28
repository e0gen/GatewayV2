using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransactionAmount", Schema = "Trans")]
public partial class TransactionAmount
{
    [Key]
    [Column("TransactionAmount_id")]
    public int TransactionAmountId { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    [Column("SetSettlement_id")]
    public int? SetSettlementId { get; set; }

    [Column("SettlementType_id")]
    public byte SettlementTypeId { get; set; }

    [Column("Settlement_id")]
    public int? SettlementId { get; set; }

    [Precision(2)]
    public DateTime? SettlementDate { get; set; }

    [Column("AmountType_id")]
    public byte AmountTypeId { get; set; }

    public int? Installment { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CurrencyIsocode { get; set; }

    [Column(TypeName = "decimal(7, 4)")]
    public decimal? PercentValue { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal PercentAmount { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal FixedAmount { get; set; }

    [Column(TypeName = "decimal(20, 4)")]
    public decimal? Total { get; set; }

    public bool IsFee { get; set; }

    [ForeignKey("AmountTypeId")]
    [InverseProperty("TransactionAmounts")]
    public virtual AmountType AmountType { get; set; } = null!;

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("TransactionAmounts")]
    public virtual CurrencyList? CurrencyIsocodeNavigation { get; set; }

    [ForeignKey("SettlementTypeId")]
    [InverseProperty("TransactionAmounts")]
    public virtual SettlemenType SettlementType { get; set; } = null!;

    [ForeignKey("TransFailId")]
    [InverseProperty("TransactionAmounts")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("TransactionAmounts")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("TransactionAmounts")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }
}
