using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ApplicationIdentityToCurrency", Schema = "Data")]
public partial class ApplicationIdentityToCurrency
{
    [Key]
    [Column("ApplicationIdentityToCurrency_id")]
    public int ApplicationIdentityToCurrencyId { get; set; }

    [Column("ApplicationIdentity_id")]
    public int ApplicationIdentityId { get; set; }

    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("ApplicationIdentityToCurrencies")]
    public virtual ApplicationIdentity ApplicationIdentity { get; set; } = null!;

    [ForeignKey("CurrencyIsocode")]
    [InverseProperty("ApplicationIdentityToCurrencies")]
    public virtual CurrencyList CurrencyIsocodeNavigation { get; set; } = null!;
}
