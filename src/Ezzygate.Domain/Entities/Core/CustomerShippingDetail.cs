using System.ComponentModel.DataAnnotations;

namespace Ezzygate.Domain.Entities.Core;

public sealed class CustomerShippingDetail
{
    [Key]
    public int CustomerShippingDetailId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public int? AccountAddressId { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [StringLength(250)]
    public string? Comment { get; set; }

    public Customer Customer { get; set; } = null!;
    public AccountAddress? AccountAddress { get; set; }
}