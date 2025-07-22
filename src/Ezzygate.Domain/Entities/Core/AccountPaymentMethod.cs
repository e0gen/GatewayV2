using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Account payment method entity (placeholder for now)
/// </summary>
public sealed class AccountPaymentMethod
{
    [Key]
    public int AccountPaymentMethodId { get; set; }
    
    [Required]
    public int AccountId { get; set; }
    
    public int? AccountAddressId { get; set; }
    
    [Required]
    public byte PaymentMethodId { get; set; }
    
    [StringLength(50)]
    public string? Title { get; set; }
    
    public bool IsActive { get; set; }

    // Navigation properties
    public Account Account { get; set; } = null!;
    public AccountAddress? AccountAddress { get; set; }
} 