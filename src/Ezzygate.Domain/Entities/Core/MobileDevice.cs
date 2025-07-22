using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Mobile device entity (placeholder for now)
/// </summary>
public sealed class MobileDevice
{
    [Key]
    public int MobileDeviceId { get; set; }
    
    [Required]
    public int AccountId { get; set; }
    
    [Required]
    public DateTime InsertDate { get; set; }
    
    [StringLength(80)]
    public string? DeviceIdentity { get; set; }
    
    [StringLength(20)]
    public string? FriendlyName { get; set; }
    
    public bool IsActive { get; set; }

    // Navigation properties
    public Account Account { get; set; } = null!;
} 