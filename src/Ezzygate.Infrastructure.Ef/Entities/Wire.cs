using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("Wire", Schema = "Finance")]
public partial class Wire
{
    [Key]
    [Column("Wire_id")]
    public int WireId { get; set; }

    [Column("WireProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? WireProviderId { get; set; }

    [Column("WireBatch_id")]
    public int? WireBatchId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("AccountPayee_id")]
    public int? AccountPayeeId { get; set; }

    [Column("MerchantSettlement_id")]
    public int? MerchantSettlementId { get; set; }

    [Column("AffiliateSettlement_id")]
    public int? AffiliateSettlementId { get; set; }

    public bool IsShow { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(19, 2)")]
    public decimal AmountProcessed { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column("CurrencyISOCodeProcessed")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocodeProcessed { get; set; } = null!;

    [Precision(0)]
    public DateTime CreateDate { get; set; }

    [Precision(0)]
    public DateTime? WireDate { get; set; }

    [Column(TypeName = "decimal(9, 2)")]
    public decimal WireFee { get; set; }

    public byte WireStatus { get; set; }

    [StringLength(20)]
    public string? ActionUser { get; set; }

    [Precision(0)]
    public DateTime? ActionDate { get; set; }

    public bool? IsApproveLevel1 { get; set; }

    public bool? IsApproveLevel2 { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? ProviderReference1 { get; set; }

    [StringLength(50)]
    public string? TargetTitle { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? TargetBankAccountText { get; set; }

    [StringLength(1000)]
    public string? Comment { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Wires")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountPayeeId")]
    [InverseProperty("Wires")]
    public virtual AccountPayee? AccountPayee { get; set; }

    [ForeignKey("AffiliateSettlementId")]
    [InverseProperty("Wires")]
    public virtual TblAffiliatePayment? AffiliateSettlement { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("WireCurrencyIsocodeNavigations")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("CurrencyIsocodeProcessed")]
    [InverseProperty("WireCurrencyIsocodeProcessedNavigations")]
    public virtual CurrencyList CurrencyIsocodeProcessedNavigation { get; set; } = null!;

    [ForeignKey("MerchantSettlementId")]
    [InverseProperty("Wires")]
    public virtual TblTransactionPay? MerchantSettlement { get; set; }

    [ForeignKey("WireBatchId")]
    [InverseProperty("Wires")]
    public virtual WireBatch? WireBatch { get; set; }

    [InverseProperty("Wire")]
    public virtual ICollection<WireHistory> WireHistories { get; set; } = new List<WireHistory>();

    [ForeignKey("WireProviderId")]
    [InverseProperty("Wires")]
    public virtual WireProvider? WireProvider { get; set; }
}
