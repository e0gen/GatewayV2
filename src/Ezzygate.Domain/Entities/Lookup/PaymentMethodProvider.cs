using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Core;

namespace Ezzygate.Domain.Entities.Lookup;

/// <summary>
/// Payment method provider lookup entity
/// </summary>
public sealed class PaymentMethodProvider
{
    [Key]
    [StringLength(16)]
    public string PaymentMethodProviderId { get; set; } = null!;
    
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Required]
    public bool IsActive { get; set; }

    // Collections
    public ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();
} 