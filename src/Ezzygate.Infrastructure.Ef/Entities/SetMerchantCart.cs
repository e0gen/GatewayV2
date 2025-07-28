using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("SetMerchantCart", Schema = "Setting")]
public partial class SetMerchantCart
{
    [Key]
    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsKeepCart { get; set; }

    public bool IsAllowPreAuth { get; set; }

    public bool IsAllowDynamicProduct { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("SetMerchantCart")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
