using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("TransPaymentBillingAddress", Schema = "Trans")]
public partial class TransPaymentBillingAddress
{
    [Key]
    [Column("TransPaymentBillingAddress_id")]
    public int TransPaymentBillingAddressId { get; set; }

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
    public string? CountryIsocode { get; set; }

    [Column("CreditCard_id")]
    public int? CreditCardId { get; set; }

    [Column("Echeck_id")]
    public int? EcheckId { get; set; }

    [ForeignKey("CountryIsocode")]
    [InverseProperty("TransPaymentBillingAddresses")]
    public virtual CountryList? CountryIsocodeNavigation { get; set; }

    [ForeignKey("StateIsocode")]
    [InverseProperty("TransPaymentBillingAddresses")]
    public virtual StateList? StateIsocodeNavigation { get; set; }

    [InverseProperty("TransPaymentBillingAddress")]
    public virtual ICollection<TblCompanyTransCrypto> TblCompanyTransCryptos { get; set; } = new List<TblCompanyTransCrypto>();

    [InverseProperty("TransPaymentBillingAddress")]
    public virtual ICollection<TransPaymentMethod> TransPaymentMethods { get; set; } = new List<TransPaymentMethod>();
}
