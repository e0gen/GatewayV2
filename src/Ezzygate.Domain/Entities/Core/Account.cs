using System.ComponentModel.DataAnnotations;
using Ezzygate.Domain.Entities.Lookup;

namespace Ezzygate.Domain.Entities.Core;

/// <summary>
/// Main account entity representing user accounts in the system
/// </summary>
public sealed class Account
{
    [Key]
    public int AccountId { get; set; }
    
    [Required]
    public byte AccountTypeId { get; set; }
    
    public int? MerchantId { get; set; }
    
    public int? CustomerId { get; set; }
    
    public int? AffiliateId { get; set; }
    
    public int? DebitCompanyId { get; set; }
    
    public int? PersonalAddressId { get; set; }
    
    public int? BusinessAddressId { get; set; }
    
    [StringLength(16)]
    public string? PreferredWireProviderId { get; set; }
    
    [StringLength(7)]
    public string? AccountNumber { get; set; }
    
    public int? LoginAccountId { get; set; }
    
    [StringLength(64)]
    public string? PincodeSHA256 { get; set; }
    
    [StringLength(100)]
    public string? Name { get; set; }
    
    [StringLength(32)]
    public string? HashKey { get; set; }
    
    public short? TimeZoneOffsetUI { get; set; }
    
    [StringLength(3)]
    public string? DefaultCurrencyISOCode { get; set; }

    // Navigation properties
    public AccountType? AccountType { get; set; }
    public Customer? Customer { get; set; }
    public LoginAccount? LoginAccount { get; set; }
    public AccountAddress? PersonalAddress { get; set; }
    public AccountAddress? BusinessAddress { get; set; }

    // Collections
    public ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();
    public ICollection<AccountSubUser> AccountSubUsers { get; set; } = new List<AccountSubUser>();
    public ICollection<MobileDevice> MobileDevices { get; set; } = new List<MobileDevice>();
} 