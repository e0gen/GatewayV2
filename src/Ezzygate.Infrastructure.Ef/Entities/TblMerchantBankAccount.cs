using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[PrimaryKey("MbaMerchant", "MbaCurrency")]
[Table("tblMerchantBankAccount")]
public partial class TblMerchantBankAccount
{
    [Key]
    [Column("mba_Merchant")]
    public int MbaMerchant { get; set; }

    [Key]
    [Column("mba_Currency")]
    public int MbaCurrency { get; set; }

    [Column("mba_ABA")]
    [StringLength(80)]
    public string MbaAba { get; set; } = null!;

    [Column("mba_ABA2")]
    [StringLength(80)]
    public string MbaAba2 { get; set; } = null!;

    [Column("mba_AccountName")]
    [StringLength(80)]
    public string MbaAccountName { get; set; } = null!;

    [Column("mba_AccountName2")]
    [StringLength(80)]
    public string MbaAccountName2 { get; set; } = null!;

    [Column("mba_AccountNumber")]
    [StringLength(80)]
    public string MbaAccountNumber { get; set; } = null!;

    [Column("mba_AccountNumber2")]
    [StringLength(80)]
    public string MbaAccountNumber2 { get; set; } = null!;

    [Column("mba_BankAddress")]
    [StringLength(200)]
    public string MbaBankAddress { get; set; } = null!;

    [Column("mba_BankAddress2")]
    [StringLength(80)]
    public string MbaBankAddress2 { get; set; } = null!;

    [Column("mba_BankAddressCity")]
    [StringLength(30)]
    public string MbaBankAddressCity { get; set; } = null!;

    [Column("mba_BankAddressCity2")]
    [StringLength(30)]
    public string MbaBankAddressCity2 { get; set; } = null!;

    [Column("mba_BankAddressCountry")]
    public int MbaBankAddressCountry { get; set; }

    [Column("mba_BankAddressCountry2")]
    public int MbaBankAddressCountry2 { get; set; }

    [Column("mba_BankAddressSecond")]
    [StringLength(100)]
    public string MbaBankAddressSecond { get; set; } = null!;

    [Column("mba_BankAddressSecond2")]
    [StringLength(100)]
    public string MbaBankAddressSecond2 { get; set; } = null!;

    [Column("mba_BankAddressState")]
    [StringLength(20)]
    public string MbaBankAddressState { get; set; } = null!;

    [Column("mba_BankAddressState2")]
    [StringLength(20)]
    public string MbaBankAddressState2 { get; set; } = null!;

    [Column("mba_BankAddressZip")]
    [StringLength(20)]
    public string MbaBankAddressZip { get; set; } = null!;

    [Column("mba_BankAddressZip2")]
    [StringLength(20)]
    public string MbaBankAddressZip2 { get; set; } = null!;

    [Column("mba_BankName")]
    [StringLength(80)]
    public string MbaBankName { get; set; } = null!;

    [Column("mba_BankName2")]
    [StringLength(80)]
    public string MbaBankName2 { get; set; } = null!;

    [Column("mba_IBAN")]
    [StringLength(80)]
    public string MbaIban { get; set; } = null!;

    [Column("mba_IBAN2")]
    [StringLength(80)]
    public string MbaIban2 { get; set; } = null!;

    [Column("mba_SepaBic")]
    [StringLength(11)]
    [Unicode(false)]
    public string MbaSepaBic { get; set; } = null!;

    [Column("mba_SepaBic2")]
    [StringLength(11)]
    [Unicode(false)]
    public string MbaSepaBic2 { get; set; } = null!;

    [Column("mba_SortCode")]
    [StringLength(80)]
    public string MbaSortCode { get; set; } = null!;

    [Column("mba_SortCode2")]
    [StringLength(80)]
    public string MbaSortCode2 { get; set; } = null!;

    [Column("mba_SwiftNumber")]
    [StringLength(80)]
    public string MbaSwiftNumber { get; set; } = null!;

    [Column("mba_SwiftNumber2")]
    [StringLength(80)]
    public string MbaSwiftNumber2 { get; set; } = null!;
}
