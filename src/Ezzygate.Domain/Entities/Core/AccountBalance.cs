using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Account balance entity for tracking balance changes
/// </summary>
public sealed class AccountBalance
{
    [Key]
    public int AccountBalanceId { get; set; }
    
    [Required]
    public int AccountId { get; set; }
    
    [Required]
    [StringLength(30)]
    public string BalanceSourceTypeId { get; set; } = string.Empty;
    
    public int? SourceID { get; set; }
    
    [Required]
    [StringLength(3)]
    public string CurrencyISOCode { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalBalance { get; set; }
    
    [Required]
    public DateTime InsertDate { get; set; }
    
    [StringLength(200)]
    public string? SystemText { get; set; }
    
    [Required]
    public bool IsPending { get; set; }

    // Computed properties
    public bool IsCredit => Amount > 0;
    public bool IsDebit => Amount < 0;

    // Navigation properties
    public Account Account { get; set; } = null!;
    public BalanceSourceType? BalanceSourceType { get; set; }
    public CurrencyList? Currency { get; set; }
} 