using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ApplicationIdentityToken", Schema = "Data")]
public partial class ApplicationIdentityToken
{
    [Key]
    [Column("ApplicationIdentityToken_id")]
    public int ApplicationIdentityTokenId { get; set; }

    [Column("ApplicationIdentity_id")]
    public int ApplicationIdentityId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("IPAddressAllowed")]
    [StringLength(50)]
    [Unicode(false)]
    public string? IpaddressAllowed { get; set; }

    public Guid Token { get; set; }

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("ApplicationIdentityTokens")]
    public virtual ApplicationIdentity ApplicationIdentity { get; set; } = null!;
}
