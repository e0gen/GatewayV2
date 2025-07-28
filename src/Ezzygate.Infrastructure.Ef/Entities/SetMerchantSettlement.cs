using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantSettlement", Schema = "Setting")]
[Index("MerchantId", "CurrencyId", Name = "UIX_SetMerchantSettlementMerchantCurrency", IsUnique = true)]
public partial class SetMerchantSettlement
{
    [Key]
    [Column("SetMerchantSettlement_id")]
    public int SetMerchantSettlementId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("Currency_id")]
    public int CurrencyId { get; set; }

    [Column(TypeName = "money")]
    public decimal WireFee { get; set; }

    [Column(TypeName = "money")]
    public decimal WireFeePercent { get; set; }

    [Column(TypeName = "money")]
    public decimal StorageFee { get; set; }

    [Column(TypeName = "money")]
    public decimal HandlingFee { get; set; }

    [Column(TypeName = "money")]
    public decimal MinPayoutAmount { get; set; }

    [Column(TypeName = "money")]
    public decimal MinSettlementAmount { get; set; }

    public bool IsAutoInvoice { get; set; }

    public bool IsShowToSettle { get; set; }

    public bool IsWireExcludeDebit { get; set; }

    public bool IsWireExcludeRefund { get; set; }

    public bool IsWireExcludeFee { get; set; }

    public bool IsWireExcludeChb { get; set; }

    public bool IsWireExcludeCashback { get; set; }

    public DateOnly? AutoStartDate { get; set; }

    public byte? AutoIntervalTimeUnit { get; set; }

    public byte? AutoIntervalCount { get; set; }

    public DateOnly? AutoNextSettlementDate { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal PayPercent { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? CryptoWalletAddress { get; set; }

    [ForeignKey("AutoIntervalTimeUnit")]
    [InverseProperty("SetMerchantSettlements")]
    public virtual TimeUnit? AutoIntervalTimeUnitNavigation { get; set; }

    [ForeignKey("CurrencyId")]
    [InverseProperty("SetMerchantSettlements")]
    public virtual TblSystemCurrency Currency { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantSettlements")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
