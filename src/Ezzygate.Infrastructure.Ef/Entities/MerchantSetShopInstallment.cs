using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("MerchantSetShopInstallments", Schema = "Setting")]
public partial class MerchantSetShopInstallment
{
    [Key]
    [Column("MerchantSetShopInstallments_id")]
    public int MerchantSetShopInstallmentsId { get; set; }

    [Column("MerchantSetShop_id")]
    public int MerchantSetShopId { get; set; }

    [Column(TypeName = "decimal(9, 0)")]
    public decimal Amount { get; set; }

    public byte MaxInstallments { get; set; }

    [ForeignKey("MerchantSetShopId")]
    [InverseProperty("MerchantSetShopInstallments")]
    public virtual MerchantSetShop MerchantSetShop { get; set; } = null!;
}
