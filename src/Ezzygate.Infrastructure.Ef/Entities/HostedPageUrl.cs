using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("HostedPageURL", Schema = "Data")]
public partial class HostedPageUrl
{
    [Key]
    [Column("HostedPageURL_id")]
    public int HostedPageUrlId { get; set; }

    [Column("HostedPageType_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string HostedPageTypeId { get; set; } = null!;

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [StringLength(250)]
    public string? PageTitle { get; set; }

    [StringLength(1000)]
    public string? PageDescription { get; set; }

    [Column("URL")]
    [StringLength(4000)]
    public string? Url { get; set; }

    public bool IsEnabled { get; set; }

    public DateTime? InsertDate { get; set; }

    [StringLength(20)]
    public string? InsertUserName { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("HostedPageUrls")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
