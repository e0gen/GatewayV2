using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountMsgInbox", Schema = "Data")]
public partial class AccountMsgInbox
{
    [Key]
    [Column("AccountMsgInbox_id")]
    public int AccountMsgInboxId { get; set; }

    [Column("AccountMsg_id")]
    public int AccountMsgId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    public bool MessageIsRead { get; set; }

    public bool MessageIsArchived { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("AccountMsgInboxes")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("AccountMsgId")]
    [InverseProperty("AccountMsgInboxes")]
    public virtual AccountMsg AccountMsg { get; set; } = null!;
}
