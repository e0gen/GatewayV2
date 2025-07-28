using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblBillingAddress")]
public partial class TblBillingAddress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("insertDate", TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("address1")]
    [StringLength(100)]
    public string Address1 { get; set; } = null!;

    [Column("address2")]
    [StringLength(100)]
    public string Address2 { get; set; } = null!;

    [Column("city")]
    [StringLength(60)]
    public string City { get; set; } = null!;

    [Column("zipCode")]
    [StringLength(15)]
    public string ZipCode { get; set; } = null!;

    [Column("stateId")]
    public int StateId { get; set; }

    [Column("countryId")]
    public int CountryId { get; set; }

    [Column("stateIso")]
    [StringLength(2)]
    public string StateIso { get; set; } = null!;

    [Column("countryIso")]
    [StringLength(2)]
    public string CountryIso { get; set; } = null!;

    [InverseProperty("BillingAddress")]
    public virtual ICollection<PhoneDetail> PhoneDetails { get; set; } = new List<PhoneDetail>();

    [InverseProperty("BillingAddress")]
    public virtual ICollection<TblCheckDetail> TblCheckDetails { get; set; } = new List<TblCheckDetail>();

    [InverseProperty("BillingAddress")]
    public virtual ICollection<TblCreditCard> TblCreditCards { get; set; } = new List<TblCreditCard>();
}
