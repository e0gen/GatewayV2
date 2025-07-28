using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCCStorage")]
public partial class TblCcstorage
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("companyID")]
    public int? CompanyId { get; set; }

    public short PaymentMethod { get; set; }

    [Column("payID")]
    public int PayId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime InsertDate { get; set; }

    [Column("CHPersonalNum")]
    [StringLength(50)]
    public string ChpersonalNum { get; set; } = null!;

    [Column("ExpMM")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpMm { get; set; } = null!;

    [Column("ExpYY")]
    [StringLength(10)]
    [Unicode(false)]
    public string ExpYy { get; set; } = null!;

    [Column("cc_cui")]
    [StringLength(10)]
    public string CcCui { get; set; } = null!;

    [Column("CHPhoneNumber")]
    [StringLength(50)]
    public string ChphoneNumber { get; set; } = null!;

    [Column("CHEmail")]
    [StringLength(80)]
    public string Chemail { get; set; } = null!;

    [Column("CHFullName")]
    [StringLength(100)]
    public string ChfullName { get; set; } = null!;

    [Column("CCard_display")]
    [StringLength(50)]
    public string CcardDisplay { get; set; } = null!;

    [Column("IPAddress")]
    [StringLength(50)]
    public string Ipaddress { get; set; } = null!;

    [Column("isDeleted")]
    public bool IsDeleted { get; set; }

    [StringLength(500)]
    public string Comment { get; set; } = null!;

    [Column("CCard_number256")]
    [MaxLength(200)]
    public byte[]? CcardNumber256 { get; set; }

    [Column("CCardLast4")]
    [StringLength(4)]
    public string? CcardLast4 { get; set; }

    [Column("CHStreet")]
    [StringLength(100)]
    public string? Chstreet { get; set; }

    [Column("CHSCity")]
    [StringLength(60)]
    public string? Chscity { get; set; }

    [Column("CHSZipCode")]
    [StringLength(20)]
    public string? ChszipCode { get; set; }

    [Column("stateId")]
    public int? StateId { get; set; }

    [Column("countryId")]
    public int? CountryId { get; set; }

    [Column("BINCountry")]
    [StringLength(2)]
    public string? Bincountry { get; set; }

    [Column("CHStreet1")]
    [StringLength(100)]
    public string? Chstreet1 { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCcstorages")]
    public virtual TblCompany? Company { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("TblCcstorages")]
    public virtual CountryList? Country { get; set; }

    [ForeignKey("PaymentMethod")]
    [InverseProperty("TblCcstorages")]
    public virtual PaymentMethod PaymentMethodNavigation { get; set; } = null!;

    [ForeignKey("StateId")]
    [InverseProperty("TblCcstorages")]
    public virtual StateList? State { get; set; }
}
