using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Shopping cart entity (placeholder for now)
/// </summary>
public sealed class Cart
{
    [Key]
    public int CartId { get; set; }
    
    [Required]
    public int MerchantId { get; set; }
    
    public int? CustomerId { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    public DateTime? CheckoutDate { get; set; }
    
    [Required]
    [StringLength(3)]
    public string CurrencyISOCode { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalProducts { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalShipping { get; set; }

    // Computed properties
    public decimal Total => TotalProducts + TotalShipping;

    // Navigation properties
    public Customer? Customer { get; set; }
} 