using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("Account", Schema = "Data")]
public partial class Account
{
    [Key]
    [Column("Account_id")]
    public int AccountId { get; set; }

    [Column("AccountType_id")]
    public byte AccountTypeId { get; set; }

    [Column("Merchant_id")]
    public int? MerchantId { get; set; }

    [Column("Customer_id")]
    public int? CustomerId { get; set; }

    [Column("Affiliate_id")]
    public int? AffiliateId { get; set; }

    [Column("DebitCompany_id")]
    public int? DebitCompanyId { get; set; }

    [Column("PersonalAddress_id")]
    public int? PersonalAddressId { get; set; }

    [Column("BusinessAddress_id")]
    public int? BusinessAddressId { get; set; }

    [Column("PreferredWireProvider_id")]
    [StringLength(16)]
    [Unicode(false)]
    public string? PreferredWireProviderId { get; set; }

    [StringLength(7)]
    [Unicode(false)]
    public string? AccountNumber { get; set; }

    [Column("LoginAccount_id")]
    public int? LoginAccountId { get; set; }

    [Column("PincodeSHA256")]
    [StringLength(64)]
    [Unicode(false)]
    public string? PincodeSha256 { get; set; }

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(32)]
    public string? HashKey { get; set; }

    [Column("TimeZoneOffsetUI")]
    public short? TimeZoneOffsetUi { get; set; }

    [Column("DefaultCurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string? DefaultCurrencyIsocode { get; set; }

    [InverseProperty("SourceAccount")]
    public virtual ICollection<AccountBalanceMoneyRequest> AccountBalanceMoneyRequestSourceAccounts { get; set; } = new List<AccountBalanceMoneyRequest>();

    [InverseProperty("TargetAccount")]
    public virtual ICollection<AccountBalanceMoneyRequest> AccountBalanceMoneyRequestTargetAccounts { get; set; } = new List<AccountBalanceMoneyRequest>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountBankAccount> AccountBankAccounts { get; set; } = new List<AccountBankAccount>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountExternalService> AccountExternalServices { get; set; } = new List<AccountExternalService>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountFile> AccountFiles { get; set; } = new List<AccountFile>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountMsgInbox> AccountMsgInboxes { get; set; } = new List<AccountMsgInbox>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountNote> AccountNotes { get; set; } = new List<AccountNote>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountPayee> AccountPayees { get; set; } = new List<AccountPayee>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountPaymentMethod> AccountPaymentMethods { get; set; } = new List<AccountPaymentMethod>();

    [InverseProperty("Account")]
    public virtual ICollection<AccountSubUser> AccountSubUsers { get; set; } = new List<AccountSubUser>();

    [ForeignKey("AccountTypeId")]
    [InverseProperty("Accounts")]
    public virtual AccountType AccountType { get; set; } = null!;

    [ForeignKey("AffiliateId")]
    [InverseProperty("Account")]
    public virtual TblAffiliate? Affiliate { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<AppModuleAccountSetting> AppModuleAccountSettings { get; set; } = new List<AppModuleAccountSetting>();

    [ForeignKey("BusinessAddressId")]
    [InverseProperty("AccountBusinessAddresses")]
    public virtual AccountAddress? BusinessAddress { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Account")]
    public virtual Customer? Customer { get; set; }

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("Account")]
    public virtual TblDebitCompany? DebitCompany { get; set; }

    [ForeignKey("DefaultCurrencyIsocode")]
    [InverseProperty("Accounts")]
    public virtual CurrencyList? DefaultCurrencyIsocodeNavigation { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<ExternalServiceHistory> ExternalServiceHistories { get; set; } = new List<ExternalServiceHistory>();

    [ForeignKey("LoginAccountId")]
    [InverseProperty("Accounts")]
    public virtual LoginAccount? LoginAccount { get; set; }

    [ForeignKey("MerchantId")]
    [InverseProperty("Account")]
    public virtual TblCompany? Merchant { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<MobileDevice> MobileDevices { get; set; } = new List<MobileDevice>();

    [ForeignKey("PersonalAddressId")]
    [InverseProperty("AccountPersonalAddresses")]
    public virtual AccountAddress? PersonalAddress { get; set; }

    [ForeignKey("PreferredWireProviderId")]
    [InverseProperty("Accounts")]
    public virtual WireProvider? PreferredWireProvider { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<RiskRuleHistory> RiskRuleHistories { get; set; } = new List<RiskRuleHistory>();

    [InverseProperty("Account")]
    public virtual ICollection<SetPeriodicFee> SetPeriodicFees { get; set; } = new List<SetPeriodicFee>();

    [InverseProperty("Account")]
    public virtual ICollection<SetRiskRule> SetRiskRules { get; set; } = new List<SetRiskRule>();

    [InverseProperty("Account")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();

    [InverseProperty("Account")]
    public virtual ICollection<SetTransactionFloor> SetTransactionFloors { get; set; } = new List<SetTransactionFloor>();

    [ForeignKey("TimeZoneOffsetUi")]
    [InverseProperty("Accounts")]
    public virtual TimeZone? TimeZoneOffsetUiNavigation { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Wire> Wires { get; set; } = new List<Wire>();

    [ForeignKey("AccountId")]
    [InverseProperty("Accounts")]
    public virtual ICollection<AdminUser> AdminUsers { get; set; } = new List<AdminUser>();
}
