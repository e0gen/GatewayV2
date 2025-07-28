using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitRule")]
public partial class TblDebitRule
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("dr_IsActive")]
    public bool DrIsActive { get; set; }

    [Column("dr_DebitCompany")]
    public int DrDebitCompany { get; set; }

    [Column("dr_ReplyCodes")]
    [StringLength(100)]
    public string DrReplyCodes { get; set; } = null!;

    [Column("dr_NotifyUsers")]
    [StringLength(100)]
    public string DrNotifyUsers { get; set; } = null!;

    [Column("dr_AttemptCount")]
    public int DrAttemptCount { get; set; }

    [Column("dr_FailCount")]
    public int DrFailCount { get; set; }

    [Column("dr_IsAutoDisable")]
    public bool DrIsAutoDisable { get; set; }

    [Column("dr_IsAutoEnable")]
    public bool DrIsAutoEnable { get; set; }

    [Column("dr_AutoEnableMinutes")]
    public int DrAutoEnableMinutes { get; set; }

    [Column("dr_AutoEnableAttempts")]
    public int DrAutoEnableAttempts { get; set; }

    [Column("dr_LastFailDate", TypeName = "datetime")]
    public DateTime DrLastFailDate { get; set; }

    [Column("dr_LastUnblockDate", TypeName = "datetime")]
    public DateTime DrLastUnblockDate { get; set; }

    [Column("dr_NotifyUsersSMS")]
    [StringLength(100)]
    public string DrNotifyUsersSms { get; set; } = null!;

    [Column("dr_Rating")]
    public int DrRating { get; set; }
}
