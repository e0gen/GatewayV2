namespace Ezzygate.Domain.Models;

public class MobileDevice
{
    public int MobileDeviceId { get; set; }
    public DateTime InsertDate { get; set; }
    public string? DeviceIdentity { get; set; }
    public string? DeviceUserAgent { get; set; }
    public string? DevicePhoneNumber { get; set; }
    public string? PassCode { get; set; }
    public DateTime? LastLogin { get; set; }
    public bool IsActivated { get; set; }
    public bool IsActive { get; set; }
    public string? AppVersion { get; set; }
    public byte SignatureFailCount { get; set; }
    public string? FriendlyName { get; set; }
    public int? AccountSubUserId { get; set; }
    public int AccountId { get; set; }
    public string? AppPushToken { get; set; }
} 