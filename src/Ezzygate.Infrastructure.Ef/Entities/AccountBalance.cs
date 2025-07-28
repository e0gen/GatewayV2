using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountBalance", Schema = "Data")]
[Index("AccountId", "CurrencyIsocode", Name = "IX_AccountBalance_AccountID_CurrencyISOCode")]
public partial class AccountBalance
{
    [Key]
    [Column("AccountBalance_id")]
    public int AccountBalanceId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("BalanceSourceType_id")]
    [StringLength(30)]
    [Unicode(false)]
    public string BalanceSourceTypeId { get; set; } = null!;

    [Column("SourceID")]
    public int? SourceId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalBalance { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(200)]
    public string? SystemText { get; set; }

    public bool IsPending { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountBalances")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("SourceAccountBalance")]
    public virtual ICollection<AccountBalanceMoneyRequest> AccountBalanceMoneyRequestSourceAccountBalances { get; set; } = new List<AccountBalanceMoneyRequest>();

    [InverseProperty("TargetAccountBalance")]
    public virtual ICollection<AccountBalanceMoneyRequest> AccountBalanceMoneyRequestTargetAccountBalances { get; set; } = new List<AccountBalanceMoneyRequest>();

    [ForeignKey("BalanceSourceTypeId")]
    [InverseProperty("AccountBalances")]
    public virtual BalanceSourceType BalanceSourceType { get; set; } = null!;

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("AccountBalances")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;
}
