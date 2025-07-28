using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransHistory", Schema = "Trans")]
public partial class TransHistory
{
    [Key]
    [Column("TransHistory_id")]
    public int TransHistoryId { get; set; }

    [Column("TransHistoryType_id")]
    public byte TransHistoryTypeId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }

    public bool? IsSucceeded { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    public int? ReferenceNumber { get; set; }

    [StringLength(500)]
    public string? ReferenceUrl { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("TransHistories")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("TransFailId")]
    [InverseProperty("TransHistories")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransHistoryTypeId")]
    [InverseProperty("TransHistories")]
    public virtual TransHistoryType TransHistoryType { get; set; } = null!;

    [ForeignKey("TransPassId")]
    [InverseProperty("TransHistories")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("TransHistories")]
    public virtual TblCompanyTransPending? TransPending { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("TransHistories")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }
}
