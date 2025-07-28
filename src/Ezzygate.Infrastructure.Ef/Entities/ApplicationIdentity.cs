using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("ApplicationIdentity", Schema = "Data")]
public partial class ApplicationIdentity
{
    [Key]
    [Column("ApplicationIdentity_id")]
    public int ApplicationIdentityId { get; set; }

    [StringLength(20)]
    public string? Name { get; set; }

    [StringLength(30)]
    public string? BrandName { get; set; }

    [StringLength(30)]
    public string? CompanyName { get; set; }

    [StringLength(30)]
    public string? Domain { get; set; }

    [StringLength(50)]
    public string? ThemeName { get; set; }

    [StringLength(20)]
    public string? ContentFolder { get; set; }

    [Column("URLDevCenter")]
    [StringLength(50)]
    public string? UrldevCenter { get; set; }

    [Column("URLProcess")]
    [StringLength(50)]
    public string? Urlprocess { get; set; }

    [Column("URLMerchantCP")]
    [StringLength(50)]
    public string? UrlmerchantCp { get; set; }

    [Column("URLWallet")]
    [StringLength(50)]
    public string? Urlwallet { get; set; }

    [Column("URLWebsite")]
    [StringLength(50)]
    public string? Urlwebsite { get; set; }

    [StringLength(20)]
    public string? SmtpServer { get; set; }

    [StringLength(20)]
    public string? SmtpUsername { get; set; }

    [StringLength(20)]
    public string? SmtpPassword { get; set; }

    [StringLength(50)]
    public string? EmailFrom { get; set; }

    [StringLength(50)]
    public string? EmailContactTo { get; set; }

    [StringLength(300)]
    public string? CopyRightText { get; set; }

    public bool IsActive { get; set; }

    [Column("WireAccount_id")]
    public short? WireAccountId { get; set; }

    [StringLength(32)]
    public string? HashKey { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ProcessMerchantNumber { get; set; }

    [InverseProperty("ApplicationIdentity")]
    public virtual ICollection<ApplicationIdentityToCurrency> ApplicationIdentityToCurrencies { get; set; } = new List<ApplicationIdentityToCurrency>();

    [InverseProperty("ApplicationIdentity")]
    public virtual ICollection<ApplicationIdentityToMerchantGroup> ApplicationIdentityToMerchantGroups { get; set; } = new List<ApplicationIdentityToMerchantGroup>();

    [InverseProperty("ApplicationIdentity")]
    public virtual ICollection<ApplicationIdentityToPaymentMethod> ApplicationIdentityToPaymentMethods { get; set; } = new List<ApplicationIdentityToPaymentMethod>();

    [InverseProperty("ApplicationIdentity")]
    public virtual ICollection<ApplicationIdentityToken> ApplicationIdentityTokens { get; set; } = new List<ApplicationIdentityToken>();

    [InverseProperty("ApplicationIdentity")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [ForeignKey("WireAccountId")]
    [InverseProperty("ApplicationIdentities")]
    public virtual WireAccount? WireAccount { get; set; }

    [ForeignKey("ApplicationIdentityId")]
    [InverseProperty("ApplicationIdentities")]
    public virtual ICollection<TblAffiliate> Affiliates { get; set; } = new List<TblAffiliate>();
}
