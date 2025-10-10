using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanyMakePaymentsProfiles")]
public partial class TblCompanyMakePaymentsProfile
{
    [Key]
    [Column("CompanyMakePaymentsProfiles_id")]
    public int CompanyMakePaymentsProfilesId { get; set; }

    public byte ProfileType { get; set; }

    [Column("basicInfo_costumerNumber")]
    [StringLength(10)]
    public string BasicInfoCostumerNumber { get; set; } = null!;

    [Column("basicInfo_costumerName")]
    [StringLength(50)]
    public string BasicInfoCostumerName { get; set; } = null!;

    [Column("basicInfo_contactPersonName")]
    [StringLength(50)]
    public string BasicInfoContactPersonName { get; set; } = null!;

    [Column("basicInfo_phoneNumber")]
    [StringLength(25)]
    public string BasicInfoPhoneNumber { get; set; } = null!;

    [Column("basicInfo_faxNumber")]
    [StringLength(25)]
    public string BasicInfoFaxNumber { get; set; } = null!;

    [Column("basicInfo_email")]
    [StringLength(50)]
    public string BasicInfoEmail { get; set; } = null!;

    [Column("basicInfo_address")]
    [StringLength(100)]
    public string BasicInfoAddress { get; set; } = null!;

    [Column("basicInfo_comment")]
    [StringLength(80)]
    public string BasicInfoComment { get; set; } = null!;

    [Column("bankIsraelInfo_PayeeName")]
    [StringLength(80)]
    public string BankIsraelInfoPayeeName { get; set; } = null!;

    [Column("bankIsraelInfo_CompanyLegalNumber")]
    [StringLength(80)]
    public string BankIsraelInfoCompanyLegalNumber { get; set; } = null!;

    [Column("bankIsraelInfo_personalIdNumber")]
    [StringLength(80)]
    public string BankIsraelInfoPersonalIdNumber { get; set; } = null!;

    [Column("bankIsraelInfo_bankBranch")]
    [StringLength(5)]
    public string BankIsraelInfoBankBranch { get; set; } = null!;

    [Column("bankIsraelInfo_AccountNumber")]
    [StringLength(80)]
    public string BankIsraelInfoAccountNumber { get; set; } = null!;

    [Column("bankIsraelInfo_PaymentMethod")]
    [StringLength(80)]
    public string BankIsraelInfoPaymentMethod { get; set; } = null!;

    [Column("bankAbroadAccountName")]
    [StringLength(80)]
    public string BankAbroadAccountName { get; set; } = null!;

    [Column("bankAbroadAccountNumber")]
    [StringLength(80)]
    public string BankAbroadAccountNumber { get; set; } = null!;

    [Column("bankAbroadBankName")]
    [StringLength(80)]
    public string BankAbroadBankName { get; set; } = null!;

    [Column("bankAbroadBankAddress")]
    [StringLength(200)]
    public string BankAbroadBankAddress { get; set; } = null!;

    [Column("bankAbroadSwiftNumber")]
    [StringLength(80)]
    public string BankAbroadSwiftNumber { get; set; } = null!;

    [Column("bankAbroadIBAN")]
    [StringLength(80)]
    public string BankAbroadIban { get; set; } = null!;

    [Column("bankAbroadABA")]
    [StringLength(80)]
    public string BankAbroadAba { get; set; } = null!;

    [Column("bankAbroadSortCode")]
    [StringLength(80)]
    public string BankAbroadSortCode { get; set; } = null!;

    [Column("bankAbroadAccountName2")]
    [StringLength(80)]
    public string BankAbroadAccountName2 { get; set; } = null!;

    [Column("bankAbroadAccountNumber2")]
    [StringLength(80)]
    public string BankAbroadAccountNumber2 { get; set; } = null!;

    [Column("bankAbroadBankName2")]
    [StringLength(80)]
    public string BankAbroadBankName2 { get; set; } = null!;

    [Column("bankAbroadBankAddress2")]
    [StringLength(200)]
    public string BankAbroadBankAddress2 { get; set; } = null!;

    [Column("bankAbroadSwiftNumber2")]
    [StringLength(80)]
    public string BankAbroadSwiftNumber2 { get; set; } = null!;

    [Column("bankAbroadIBAN2")]
    [StringLength(80)]
    public string BankAbroadIban2 { get; set; } = null!;

    [Column("bankAbroadABA2")]
    [StringLength(80)]
    public string BankAbroadAba2 { get; set; } = null!;

    [Column("bankAbroadSortCode2")]
    [StringLength(80)]
    public string BankAbroadSortCode2 { get; set; } = null!;

    [Column("bankAbroadBankAddressSecond")]
    [StringLength(100)]
    public string BankAbroadBankAddressSecond { get; set; } = null!;

    [Column("bankAbroadBankAddressCity")]
    [StringLength(30)]
    public string BankAbroadBankAddressCity { get; set; } = null!;

    [Column("bankAbroadBankAddressState")]
    [StringLength(20)]
    public string BankAbroadBankAddressState { get; set; } = null!;

    [Column("bankAbroadBankAddressZip")]
    [StringLength(20)]
    public string BankAbroadBankAddressZip { get; set; } = null!;

    [Column("bankAbroadBankAddressCountry")]
    public int BankAbroadBankAddressCountry { get; set; }

    [Column("bankAbroadBankAddressSecond2")]
    [StringLength(100)]
    public string BankAbroadBankAddressSecond2 { get; set; } = null!;

    [Column("bankAbroadBankAddressCity2")]
    [StringLength(30)]
    public string BankAbroadBankAddressCity2 { get; set; } = null!;

    [Column("bankAbroadBankAddressState2")]
    [StringLength(20)]
    public string BankAbroadBankAddressState2 { get; set; } = null!;

    [Column("bankAbroadBankAddressZip2")]
    [StringLength(20)]
    public string BankAbroadBankAddressZip2 { get; set; } = null!;

    [Column("bankAbroadBankAddressCountry2")]
    public int BankAbroadBankAddressCountry2 { get; set; }

    [Column("bankAbroadSepaBic")]
    [StringLength(11)]
    [Unicode(false)]
    public string BankAbroadSepaBic { get; set; } = null!;

    [Column("bankAbroadSepaBic2")]
    [StringLength(11)]
    [Unicode(false)]
    public string BankAbroadSepaBic2 { get; set; } = null!;

    [Column("bankIsraelInfo_BankCode")]
    [StringLength(80)]
    public string BankIsraelInfoBankCode { get; set; } = null!;

    [Column("company_id")]
    public int? CompanyId { get; set; }

    [Column("isSystem")]
    public bool IsSystem { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyMakePaymentsProfiles")]
    public virtual TblCompany? Company { get; set; }

    [InverseProperty("CompanyMakePaymentsProfiles")]
    public virtual ICollection<TblCompanyMakePaymentsRequest> TblCompanyMakePaymentsRequests { get; set; } = new List<TblCompanyMakePaymentsRequest>();
}
