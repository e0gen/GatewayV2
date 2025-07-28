using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CartFailLog", Schema = "Data")]
public partial class CartFailLog
{
    [Key]
    [Column("CartFailLog_id")]
    public int CartFailLogId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    [Column("Cart_id")]
    public int CartId { get; set; }

    [Column("TransFail_id")]
    public int? TransFailId { get; set; }

    [Precision(0)]
    public DateTime InsertDate { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartFailLogs")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("CartFailLogs")]
    public virtual TblCompany Merchant { get; set; } = null!;

    [ForeignKey("TransFailId")]
    [InverseProperty("CartFailLogs")]
    public virtual TblCompanyTransFail? TransFail { get; set; }
}
