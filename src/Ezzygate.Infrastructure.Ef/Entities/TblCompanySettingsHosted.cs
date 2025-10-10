using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblCompanySettingsHosted")]
public partial class TblCompanySettingsHosted
{
    [Key]
    [Column("CompanyID")]
    public int CompanyId { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsCreditCard { get; set; }

    public bool IsCreditCardRequiredEmail { get; set; }

    public bool IsCreditCardRequiredPhone { get; set; }

    [Column("IsCreditCardRequiredID")]
    public bool IsCreditCardRequiredId { get; set; }

    [Column("IsCreditCardRequiredCVV")]
    public bool IsCreditCardRequiredCvv { get; set; }

    public bool IsEcheck { get; set; }

    public bool IsEcheckRequiredEmail { get; set; }

    public bool IsEcheckRequiredPhone { get; set; }

    [Column("IsEcheckRequiredID")]
    public bool IsEcheckRequiredId { get; set; }

    public bool IsDirectDebit { get; set; }

    public bool IsDirectDebitRequiredEmail { get; set; }

    public bool IsDirectDebitRequiredPhone { get; set; }

    [Column("IsDirectDebitRequiredID")]
    public bool IsDirectDebitRequiredId { get; set; }

    public bool IsCustomer { get; set; }

    public bool IsSignatureOptional { get; set; }

    public bool IsPhoneDebit { get; set; }

    public bool IsPhoneDebitRequiredPhone { get; set; }

    public bool IsPhoneDebitRequiredEmail { get; set; }

    public bool HideCustomParametersInRedirection { get; set; }

    [StringLength(50)]
    public string? WebMoneySecretKey { get; set; }

    public bool IsWebMoney { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? WebMoneyMerchantPurse { get; set; }

    public bool? IsWhiteLabel { get; set; }

    public bool IsShowAddress1 { get; set; }

    public bool IsShowAddress2 { get; set; }

    public bool IsShowCity { get; set; }

    public bool IsShowZipCode { get; set; }

    public bool IsShowState { get; set; }

    public bool IsShowCountry { get; set; }

    public bool IsShowEmail { get; set; }

    public bool IsShowPhone { get; set; }

    public bool IsHideLanguageSwitch { get; set; }

    [Column("IsShowPersonalID")]
    public bool IsShowPersonalId { get; set; }

    public bool IsPayPal { get; set; }

    public bool IsGoogleCheckout { get; set; }

    [Column("PayPalMerchantID")]
    [StringLength(255)]
    public string? PayPalMerchantId { get; set; }

    [Column("GoogleCheckoutMerchantID")]
    [StringLength(255)]
    public string? GoogleCheckoutMerchantId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? GoogleCheckoutMerchantKey { get; set; }

    public bool IsMoneyBookers { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MoneyBookersAccount { get; set; }

    [StringLength(500)]
    public string? NotificationUrl { get; set; }

    [StringLength(500)]
    public string? RedirectionUrl { get; set; }

    [StringLength(500)]
    public string? LogoPath { get; set; }

    public byte? MerchantTextDefaultLanguage { get; set; }

    [Column("UIVersion")]
    public byte Uiversion { get; set; }

    [StringLength(20)]
    public string? ThemeCssFileName { get; set; }

    public bool IsShippingAddressRequired { get; set; }

    public bool IsShowBirthDate { get; set; }

    public bool IsGetCrypto { get; set; }

    [Column("GetCryptoMerchantID")]
    [StringLength(255)]
    public string? GetCryptoMerchantId { get; set; }

    public bool IsBitcoBrokers { get; set; }

    public bool IsGatewayStar { get; set; }

    public bool IsBtcWallet { get; set; }

    public bool IsFlutterWave { get; set; }

    [Column("FacebookID")]
    [StringLength(25)]
    [Unicode(false)]
    public string? FacebookId { get; set; }

    [Column("GoogleAnalyticsID")]
    [StringLength(25)]
    [Unicode(false)]
    public string? GoogleAnalyticsId { get; set; }

    [StringLength(200)]
    public string? LinkImage { get; set; }

    public bool IsShowMerchantDetails { get; set; }

    public bool IsShowAuthorizeCheckbox { get; set; }

    public bool IsShowConfirmationPage { get; set; }

    [Column("IsBPWallet")]
    public bool IsBpwallet { get; set; }

    [Column("BPWalletMerchantID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BpwalletMerchantId { get; set; }

    [Column("BPWalletMerchantAPIKey")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BpwalletMerchantApikey { get; set; }

    [Column("BPWalletMerchantSecretKey")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BpwalletMerchantSecretKey { get; set; }

    public bool IsApplePay { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ApplePayTerminalToken { get; set; }

    [Column("ApplePayAPILogin")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ApplePayApilogin { get; set; }

    [Column("ApplePayAPIPassword")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ApplePayApipassword { get; set; }

    public bool IsDeclineRedirect { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? GoogleCheckoutLogin { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? GoogleCheckoutPassword { get; set; }

    public bool IsTipAllowed { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? TipPercentOptions { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanySettingsHosted")]
    public virtual TblCompany Company { get; set; } = null!;

    [ForeignKey("MerchantTextDefaultLanguage")]
    [InverseProperty("TblCompanySettingsHosteds")]
    public virtual LanguageList? MerchantTextDefaultLanguageNavigation { get; set; }
}
