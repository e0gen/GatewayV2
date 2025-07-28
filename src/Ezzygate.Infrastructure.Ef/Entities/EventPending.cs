using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("EventPending")]
public partial class EventPending
{
    [Key]
    [Column("EventPending_id")]
    public int EventPendingId { get; set; }

    [Column("EventPendingType_id")]
    public short EventPendingTypeId { get; set; }

    [Column("TransPass_id")]
    public int? TransPassId { get; set; }

    [Column("TransPreAuth_id")]
    public int? TransPreAuthId { get; set; }

    [Column("TransPending_id")]
    public int? TransPendingId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("Customer_id")]
    public int? CustomerId { get; set; }

    [StringLength(1000)]
    public string? Parameters { get; set; }

    [Precision(2)]
    public DateTime InsertDate { get; set; }

    public byte TryCount { get; set; }

    public DateTime? ProcessStartTime { get; set; }

    [Column("processServer")]
    [StringLength(15)]
    public string? ProcessServer { get; set; }

    [Column("Affiliate_id")]
    public int? AffiliateId { get; set; }

    [Column("AdminUser_id")]
    public short? AdminUserId { get; set; }

    [ForeignKey("AdminUserId")]
    [InverseProperty("EventPendings")]
    public virtual AdminUser? AdminUser { get; set; }

    [ForeignKey("AffiliateId")]
    [InverseProperty("EventPendings")]
    public virtual TblAffiliate? Affiliate { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("EventPendings")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("EventPendingTypeId")]
    [InverseProperty("EventPendings")]
    public virtual EventPendingType EventPendingType { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("EventPendings")]
    public virtual TblCompany? Merchant { get; set; }

    [ForeignKey("TransFailId")]
    [InverseProperty("EventPendings")]
    public virtual TblCompanyTransFail? TransFail { get; set; }

    [ForeignKey("TransPassId")]
    [InverseProperty("EventPendings")]
    public virtual TblCompanyTransPass? TransPass { get; set; }

    [ForeignKey("TransPendingId")]
    [InverseProperty("EventPendings")]
    public virtual TblCompanyTransPending? TransPending { get; set; }

    [ForeignKey("TransPreAuthId")]
    [InverseProperty("EventPendings")]
    public virtual TblCompanyTransApproval? TransPreAuth { get; set; }
}
