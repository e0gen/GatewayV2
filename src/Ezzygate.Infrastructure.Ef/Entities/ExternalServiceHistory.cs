using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ExternalServiceHistory", Schema = "Log")]
public partial class ExternalServiceHistory
{
    [Key]
    [Column("ExternalServiceHistory_id")]
    public int ExternalServiceHistoryId { get; set; }

    [Column("ExternalServiceType_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? ExternalServiceTypeId { get; set; }

    [Column("ExternalServiceAction_id")]
    [StringLength(32)]
    [Unicode(false)]
    public string? ExternalServiceActionId { get; set; }

    [Column("Account_id")]
    public int AccountId { get; set; }

    [Precision(4)]
    public DateTime InsertDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? FileName { get; set; }

    [StringLength(2000)]
    public string? SystemText { get; set; }

    public bool IsError { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("ExternalServiceHistories")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("ExternalServiceActionId")]
    [InverseProperty("ExternalServiceHistories")]
    public virtual ExternalServiceAction? ExternalServiceAction { get; set; }

    [ForeignKey("ExternalServiceTypeId")]
    [InverseProperty("ExternalServiceHistories")]
    public virtual ExternalServiceType? ExternalServiceType { get; set; }
}
