using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CountryList", Schema = "List")]
[Index("CountryId", Name = "UIX_CountryList_CountryID", IsUnique = true)]
public partial class CountryList
{
    [Key]
    [Column("CountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryIsocode { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("ISOCode3")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Isocode3 { get; set; }

    [Column("ISONumber")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Isonumber { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string PhoneCode { get; set; } = null!;

    [Column("IBANLength")]
    public byte Ibanlength { get; set; }

    [Column("IsABARequired")]
    public bool IsAbarequired { get; set; }

    [Column("IsSEPA")]
    public bool IsSepa { get; set; }

    [StringLength(50)]
    public string? ZipCodeRegExPattern { get; set; }

    public byte? SortOrder { get; set; }

    [Required]
    [Column("CountryID")]
    public int? CountryId { get; set; }

    [Column("WorldRegionISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string? WorldRegionIsocode { get; set; }

    [InverseProperty("CountryIsocodeNavigation")]
    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    [InverseProperty("BankCountryIsocodeNavigation")]
    public virtual ICollection<AccountBankAccount> AccountBankAccounts { get; set; } = new List<AccountBankAccount>();

    [InverseProperty("IssuerCountryIsoCodeNavigation")]
    public virtual ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    [InverseProperty("CountryIsocodeNavigation")]
    public virtual ICollection<MerchantSetShopToCountryRegion> MerchantSetShopToCountryRegions { get; set; } = new List<MerchantSetShopToCountryRegion>();

    [InverseProperty("AddressCountryIsocodeNavigation")]
    public virtual ICollection<ShippingDetail> ShippingDetails { get; set; } = new List<ShippingDetail>();

    [InverseProperty("CountryIsocodeNavigation")]
    public virtual ICollection<StateList> StateLists { get; set; } = new List<StateList>();

    [InverseProperty("Country")]
    public virtual ICollection<TblCcstorage> TblCcstorages { get; set; } = new List<TblCcstorage>();

    [InverseProperty("BincountryNavigation")]
    public virtual ICollection<TblCreditCard> TblCreditCards { get; set; } = new List<TblCreditCard>();

    [InverseProperty("CountryIsocodeNavigation")]
    public virtual ICollection<TransPayerShippingDetail> TransPayerShippingDetails { get; set; } = new List<TransPayerShippingDetail>();

    [InverseProperty("CountryIsocodeNavigation")]
    public virtual ICollection<TransPaymentBillingAddress> TransPaymentBillingAddresses { get; set; } = new List<TransPaymentBillingAddress>();

    [InverseProperty("IssuerCountryIsoCodeNavigation")]
    public virtual ICollection<TransPaymentMethod> TransPaymentMethods { get; set; } = new List<TransPaymentMethod>();

    [ForeignKey("WorldRegionIsocode")]
    [InverseProperty("CountryLists")]
    public virtual WorldRegion? WorldRegionIsocodeNavigation { get; set; }

    [ForeignKey("CountryIsocode")]
    [InverseProperty("CountryIsocodes")]
    public virtual ICollection<CountryGroup> CountryGroups { get; set; } = new List<CountryGroup>();
}
