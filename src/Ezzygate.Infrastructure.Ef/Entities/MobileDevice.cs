using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MobileDevice")]
public partial class MobileDevice
{
    [Key]
    [Column("MobileDevice_id")]
    public int MobileDeviceId { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime InsertDate { get; set; }

    [StringLength(80)]
    public string? DeviceIdentity { get; set; }

    [StringLength(255)]
    public string? DeviceUserAgent { get; set; }

    [StringLength(30)]
    public string? DevicePhoneNumber { get; set; }

    [StringLength(10)]
    public string? PassCode { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? LastLogin { get; set; }

    public bool IsActivated { get; set; }

    public bool IsActive { get; set; }

    [StringLength(10)]
    public string? AppVersion { get; set; }

    public byte SignatureFailCount { get; set; }

    [StringLength(20)]
    public string? FriendlyName { get; set; }

    [Column("AccountSubUser_id")]
    public int? AccountSubUserId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? AppPushToken { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("MobileDevices")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountSubUserId")]
    [InverseProperty("MobileDevices")]
    public virtual AccountSubUser? AccountSubUser { get; set; }

    [InverseProperty("MobileDevice")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("MobileDevice")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("MobileDevice")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("MobileDevice")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();
}
