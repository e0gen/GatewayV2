using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("AbaAffiliate", "AbaCurrency")]
[Table("tblAffiliateBankAccount")]
public partial class TblAffiliateBankAccount
{
    [Key]
    [Column("aba_Affiliate")]
    public int AbaAffiliate { get; set; }

    [Key]
    [Column("aba_Currency")]
    public int AbaCurrency { get; set; }

    [Column("aba_ABA")]
    [StringLength(80)]
    public string AbaAba { get; set; } = null!;

    [Column("aba_ABA2")]
    [StringLength(80)]
    public string AbaAba2 { get; set; } = null!;

    [Column("aba_AccountName")]
    [StringLength(80)]
    public string AbaAccountName { get; set; } = null!;

    [Column("aba_AccountName2")]
    [StringLength(80)]
    public string AbaAccountName2 { get; set; } = null!;

    [Column("aba_AccountNumber")]
    [StringLength(80)]
    public string AbaAccountNumber { get; set; } = null!;

    [Column("aba_AccountNumber2")]
    [StringLength(80)]
    public string AbaAccountNumber2 { get; set; } = null!;

    [Column("aba_BankAddress")]
    [StringLength(200)]
    public string AbaBankAddress { get; set; } = null!;

    [Column("aba_BankAddress2")]
    [StringLength(80)]
    public string AbaBankAddress2 { get; set; } = null!;

    [Column("aba_BankAddressCity")]
    [StringLength(30)]
    public string AbaBankAddressCity { get; set; } = null!;

    [Column("aba_BankAddressCity2")]
    [StringLength(30)]
    public string AbaBankAddressCity2 { get; set; } = null!;

    [Column("aba_BankAddressCountry")]
    public int AbaBankAddressCountry { get; set; }

    [Column("aba_BankAddressCountry2")]
    public int AbaBankAddressCountry2 { get; set; }

    [Column("aba_BankAddressSecond")]
    [StringLength(100)]
    public string AbaBankAddressSecond { get; set; } = null!;

    [Column("aba_BankAddressSecond2")]
    [StringLength(100)]
    public string AbaBankAddressSecond2 { get; set; } = null!;

    [Column("aba_BankAddressState")]
    [StringLength(20)]
    public string AbaBankAddressState { get; set; } = null!;

    [Column("aba_BankAddressState2")]
    [StringLength(20)]
    public string AbaBankAddressState2 { get; set; } = null!;

    [Column("aba_BankAddressZip")]
    [StringLength(20)]
    public string AbaBankAddressZip { get; set; } = null!;

    [Column("aba_BankAddressZip2")]
    [StringLength(20)]
    public string AbaBankAddressZip2 { get; set; } = null!;

    [Column("aba_BankName")]
    [StringLength(80)]
    public string AbaBankName { get; set; } = null!;

    [Column("aba_BankName2")]
    [StringLength(80)]
    public string AbaBankName2 { get; set; } = null!;

    [Column("aba_IBAN")]
    [StringLength(80)]
    public string AbaIban { get; set; } = null!;

    [Column("aba_IBAN2")]
    [StringLength(80)]
    public string AbaIban2 { get; set; } = null!;

    [Column("aba_SepaBic")]
    [StringLength(11)]
    [Unicode(false)]
    public string AbaSepaBic { get; set; } = null!;

    [Column("aba_SepaBic2")]
    [StringLength(11)]
    [Unicode(false)]
    public string AbaSepaBic2 { get; set; } = null!;

    [Column("aba_SortCode")]
    [StringLength(80)]
    public string AbaSortCode { get; set; } = null!;

    [Column("aba_SortCode2")]
    [StringLength(80)]
    public string AbaSortCode2 { get; set; } = null!;

    [Column("aba_SwiftNumber")]
    [StringLength(80)]
    public string AbaSwiftNumber { get; set; } = null!;

    [Column("aba_SwiftNumber2")]
    [StringLength(80)]
    public string AbaSwiftNumber2 { get; set; } = null!;
}
