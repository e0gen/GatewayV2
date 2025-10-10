using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountBalanceMoneyRequest", Schema = "Data")]
public partial class AccountBalanceMoneyRequest
{
    [Key]
    [Column("AccountBalanceMoneyRequest_id")]
    public int AccountBalanceMoneyRequestId { get; set; }

    [Column("SourceAccount_id")]
    public int SourceAccountId { get; set; }

    [Column("SourceAccountBalance_id")]
    public int? SourceAccountBalanceId { get; set; }

    [StringLength(100)]
    public string? SourceText { get; set; }

    [Column("TargetAccount_id")]
    public int TargetAccountId { get; set; }

    [Column("TargetAccountBalance_id")]
    public int? TargetAccountBalanceId { get; set; }

    [StringLength(100)]
    public string? TargetText { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Precision(0)]
    public DateTime RequestDate { get; set; }

    [Precision(0)]
    public DateTime? ConfirmDate { get; set; }

    public bool IsPush { get; set; }

    public bool? IsApproved { get; set; }

    [Precision(0)]
    public DateTime? PaybackDate { get; set; }

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("AccountBalanceMoneyRequests")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;

    [ForeignKey("SourceAccountId")]
    [InverseProperty("AccountBalanceMoneyRequestSourceAccounts")]
    public virtual Account SourceAccount { get; set; } = null!;

    [ForeignKey("SourceAccountBalanceId")]
    [InverseProperty("AccountBalanceMoneyRequestSourceAccountBalances")]
    public virtual AccountBalance? SourceAccountBalance { get; set; }

    [ForeignKey("TargetAccountId")]
    [InverseProperty("AccountBalanceMoneyRequestTargetAccounts")]
    public virtual Account TargetAccount { get; set; } = null!;

    [ForeignKey("TargetAccountBalanceId")]
    [InverseProperty("AccountBalanceMoneyRequestTargetAccountBalances")]
    public virtual AccountBalance? TargetAccountBalance { get; set; }
}
