using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AdminUser", Schema = "System")]
public partial class AdminUser
{
    [Key]
    [Column("AdminUser_id")]
    public short AdminUserId { get; set; }

    [Column("LoginAccount_id")]
    public int? LoginAccountId { get; set; }

    [StringLength(30)]
    public string? FullName { get; set; }

    [StringLength(80)]
    public string? NotifyEmail { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? NotifyCellPhone { get; set; }

    [Column("ProfileImageURL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ProfileImageUrl { get; set; }

    public DateOnly? StartDate { get; set; }

    public bool IsAdmin { get; set; }

    public bool? IsActive { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? MultiFactorMode { get; set; }

    [Column("TimeZoneOffsetUI")]
    public short? TimeZoneOffsetUi { get; set; }

    [InverseProperty("AdminUser")]
    public virtual ICollection<AdminUserToMailbox> AdminUserToMailboxes { get; set; } = new List<AdminUserToMailbox>();

    [InverseProperty("AdminUser")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [ForeignKey("LoginAccountId")]
    [InverseProperty("AdminUsers")]
    public virtual LoginAccount? LoginAccount { get; set; }

    [ForeignKey("TimeZoneOffsetUi")]
    [InverseProperty("AdminUsers")]
    public virtual TimeZone? TimeZoneOffsetUiNavigation { get; set; }

    [ForeignKey("AdminUserId")]
    [InverseProperty("AdminUsers")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [ForeignKey("AdminUserId")]
    [InverseProperty("AdminUsers")]
    public virtual ICollection<AdminGroup> AdminGroups { get; set; } = new List<AdminGroup>();
}
