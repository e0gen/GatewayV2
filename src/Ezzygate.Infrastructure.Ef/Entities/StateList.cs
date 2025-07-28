using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("StateList", Schema = "List")]
[Index("StateId", Name = "UIX_StateList_StateID", IsUnique = true)]
public partial class StateList
{
    [Key]
    [Column("StateISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string StateIsocode { get; set; } = null!;

    [Column("CountryISOCode")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryIsocode { get; set; } = null!;

    [StringLength(60)]
    public string Name { get; set; } = null!;

    public byte? SortOrder { get; set; }

    [Required]
    [Column("StateID")]
    public int? StateId { get; set; }

    [InverseProperty("StateIsocodeNavigation")]
    public virtual ICollection<AccountAddress> AccountAddresses { get; set; } = new List<AccountAddress>();

    [InverseProperty("BankStateIsocodeNavigation")]
    public virtual ICollection<AccountBankAccount> AccountBankAccounts { get; set; } = new List<AccountBankAccount>();

    [ForeignKey("CountryIsocode")]
    [InverseProperty("StateLists")]
    public virtual CountryList CountryIsocodeNavigation { get; set; } = null!;

    [InverseProperty("AddressStateIsocodeNavigation")]
    public virtual ICollection<ShippingDetail> ShippingDetails { get; set; } = new List<ShippingDetail>();

    [InverseProperty("State")]
    public virtual ICollection<TblCcstorage> TblCcstorages { get; set; } = new List<TblCcstorage>();

    [InverseProperty("StateIsocodeNavigation")]
    public virtual ICollection<TransPayerShippingDetail> TransPayerShippingDetails { get; set; } = new List<TransPayerShippingDetail>();

    [InverseProperty("StateIsocodeNavigation")]
    public virtual ICollection<TransPaymentBillingAddress> TransPaymentBillingAddresses { get; set; } = new List<TransPaymentBillingAddress>();
}
