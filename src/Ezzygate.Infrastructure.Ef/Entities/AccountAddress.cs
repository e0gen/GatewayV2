using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("AccountAddress", Schema = "Data")]
public partial class AccountAddress
{
    [Key]
    [Column("AccountAddress_id")]
    public int AccountAddressId { get; set; }

    [StringLength(100)]
    public string? Street1 { get; set; }

    [StringLength(100)]
    public string? Street2 { get; set; }

    [StringLength(60)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? PostalCode { get; set; }

    [Column("StateISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? StateIsocode { get; set; }

    [Column("CountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryIsocode { get; set; } = null!;

    [InverseProperty("BusinessAddress")]
    public virtual ICollection<Account> AccountBusinessAddresses { get; set; } = new List<Account>();

    [InverseProperty("AccountAddress")]
    public virtual ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    [InverseProperty("PersonalAddress")]
    public virtual ICollection<Account> AccountPersonalAddresses { get; set; } = new List<Account>();

    [ForeignKey("CountryIsocode")]
    [InverseProperty("AccountAddresses")]
    public virtual CountryList CountryIsocodeNavigation { get; set; } = null!;

    [InverseProperty("AccountAddress")]
    public virtual ICollection<CustomerShippingDetail> CustomerShippingDetails { get; set; } = new List<CustomerShippingDetail>();

    [ForeignKey("StateIsocode")]
    [InverseProperty("AccountAddresses")]
    public virtual StateList? StateIsocodeNavigation { get; set; }
}
