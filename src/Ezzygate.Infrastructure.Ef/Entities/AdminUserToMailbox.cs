using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AdminUserToMailbox", Schema = "System")]
public partial class AdminUserToMailbox
{
    [Key]
    [Column("AdminUserToMailbox_id")]
    public short AdminUserToMailboxId { get; set; }

    [Column("AdminUser_id")]
    public short AdminUserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Mailbox { get; set; } = null!;

    [Column("isDefault")]
    public bool IsDefault { get; set; }

    [ForeignKey("AdminUserId")]
    [InverseProperty("AdminUserToMailboxes")]
    public virtual AdminUser AdminUser { get; set; } = null!;
}
