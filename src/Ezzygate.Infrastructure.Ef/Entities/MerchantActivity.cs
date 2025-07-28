using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantActivity", Schema = "Track")]
public partial class MerchantActivity
{
    [Key]
    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Precision(2)]
    public DateTime? DateFirstLogin { get; set; }

    [Precision(2)]
    public DateTime? DateFirstTransPass { get; set; }

    [Precision(2)]
    public DateTime? DateLastTransPass { get; set; }

    [Precision(2)]
    public DateTime? DateLastTransFail { get; set; }

    [Precision(2)]
    public DateTime? DateLastTransPreAuth { get; set; }

    [Precision(2)]
    public DateTime? DateLastTransPending { get; set; }

    [Precision(2)]
    public DateTime? DateLastSettlement { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("MerchantActivity")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
