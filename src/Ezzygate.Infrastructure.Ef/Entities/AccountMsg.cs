using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountMsg", Schema = "Data")]
public partial class AccountMsg
{
    [Key]
    [Column("AccountMsg_id")]
    public int AccountMsgId { get; set; }

    [Column("Parent_id")]
    public int? ParentId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [StringLength(250)]
    public string? MessageTitle { get; set; }

    [StringLength(2500)]
    public string? MessageText { get; set; }

    [StringLength(250)]
    public string? MessageFilePath { get; set; }

    public bool IsAdminMsg { get; set; }

    [StringLength(50)]
    public string? AdminUser { get; set; }

    [StringLength(250)]
    public string? AdminComment { get; set; }

    [InverseProperty("AccountMsg")]
    public virtual ICollection<AccountMsgInbox> AccountMsgInboxes { get; set; } = new List<AccountMsgInbox>();

    [InverseProperty("Parent")]
    public virtual ICollection<AccountMsg> InverseParent { get; set; } = new List<AccountMsg>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual AccountMsg? Parent { get; set; }
}
