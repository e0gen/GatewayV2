using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblExternalCardTerminalToMerchant")]
public partial class TblExternalCardTerminalToMerchant
{
    [Key]
    [Column("ExternalCardTerminalToMerchant_id")]
    public int ExternalCardTerminalToMerchantId { get; set; }

    [Column("ExternalCardTerminal_id")]
    public int ExternalCardTerminalId { get; set; }

    [Column("Merchant_id")]
    public int MerchantId { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("ExternalCardTerminalId")]
    [InverseProperty("TblExternalCardTerminalToMerchants")]
    public virtual TblExternalCardTerminal ExternalCardTerminal { get; set; } = null!;

    [ForeignKey("MerchantId")]
    [InverseProperty("TblExternalCardTerminalToMerchants")]
    public virtual TblCompany Merchant { get; set; } = null!;
}
