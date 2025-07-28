using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblAffiliates")]
public partial class TblAffiliate
{
    [Key]
    [Column("affiliates_id")]
    public int AffiliatesId { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("comments")]
    [StringLength(2000)]
    public string Comments { get; set; } = null!;

    [StringLength(80)]
    public string HeaderImageFileName { get; set; } = null!;

    [StringLength(80)]
    public string HeaderSmallImageFileName { get; set; } = null!;

    public bool ShowSystemPay { get; set; }

    public bool ShowRemoteCharge { get; set; }

    public bool ShowCustomerPurchase { get; set; }

    public bool ShowPublicPay { get; set; }

    public bool ShowDataUpdate { get; set; }

    public bool ShowFees { get; set; }

    public bool ShowDownloads { get; set; }

    public bool ShowPayments { get; set; }

    [Column("isAllowControlPanel")]
    public bool IsAllowControlPanel { get; set; }

    [StringLength(20)]
    public string ControlPanelUsername { get; set; } = null!;

    [Column(TypeName = "smallmoney")]
    public decimal FeeRatio { get; set; }

    [Column("AFShowMerchants")]
    public bool AfshowMerchants { get; set; }

    [Column("AFShowStats")]
    public bool AfshowStats { get; set; }

    [Column("AFTransactionApplication")]
    public bool AftransactionApplication { get; set; }

    [Column("AFSettlements")]
    public bool Afsettlements { get; set; }

    [MaxLength(200)]
    public byte[]? ControlPanelPassword256 { get; set; }

    [Column("AFCompanyID")]
    public int? AfcompanyId { get; set; }

    public bool IsActive { get; set; }

    [Column("AFLinkRefID")]
    [StringLength(50)]
    public string? AflinkRefId { get; set; }

    [StringLength(50)]
    public string? ControlPanelEmail { get; set; }

    [Column("PaymentAbroadABA")]
    [StringLength(80)]
    public string? PaymentAbroadAba { get; set; }

    [Column("PaymentAbroadABA2")]
    [StringLength(80)]
    public string? PaymentAbroadAba2 { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadAccountName { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadAccountName2 { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadAccountNumber { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadAccountNumber2 { get; set; }

    [StringLength(200)]
    public string? PaymentAbroadBankAddress { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadBankAddress2 { get; set; }

    [StringLength(30)]
    public string? PaymentAbroadBankAddressCity { get; set; }

    [StringLength(30)]
    public string? PaymentAbroadBankAddressCity2 { get; set; }

    public int? PaymentAbroadBankAddressCountry { get; set; }

    public int? PaymentAbroadBankAddressCountry2 { get; set; }

    [StringLength(100)]
    public string? PaymentAbroadBankAddressSecond { get; set; }

    [StringLength(100)]
    public string? PaymentAbroadBankAddressSecond2 { get; set; }

    [StringLength(20)]
    public string? PaymentAbroadBankAddressState { get; set; }

    [StringLength(20)]
    public string? PaymentAbroadBankAddressState2 { get; set; }

    [StringLength(20)]
    public string? PaymentAbroadBankAddressZip { get; set; }

    [StringLength(20)]
    public string? PaymentAbroadBankAddressZip2 { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadBankName { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadBankName2 { get; set; }

    [Column("PaymentAbroadIBAN")]
    [StringLength(80)]
    public string? PaymentAbroadIban { get; set; }

    [Column("PaymentAbroadIBAN2")]
    [StringLength(80)]
    public string? PaymentAbroadIban2 { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? PaymentAbroadSepaBic { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string? PaymentAbroadSepaBic2 { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadSortCode { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadSortCode2 { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadSwiftNumber { get; set; }

    [StringLength(80)]
    public string? PaymentAbroadSwiftNumber2 { get; set; }

    [Column("IDnumber")]
    [StringLength(15)]
    public string? Idnumber { get; set; }

    [StringLength(200)]
    public string? LegalName { get; set; }

    [StringLength(15)]
    public string? LegalNumber { get; set; }

    public int? PaymentReceiveCurrency { get; set; }

    public byte? PaymentReceiveType { get; set; }

    [StringLength(80)]
    public string? PaymentMethod { get; set; }

    [StringLength(80)]
    public string? PaymentPayeeName { get; set; }

    public int? PaymentBank { get; set; }

    [StringLength(80)]
    public string? PaymentBranch { get; set; }

    [StringLength(80)]
    public string? PaymentAccount { get; set; }

    [StringLength(50)]
    public string? OutboundUsername { get; set; }

    [StringLength(50)]
    public string? OutboundPassword { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [InverseProperty("Affiliate")]
    public virtual Account? Account { get; set; }

    [InverseProperty("Affiliate")]
    public virtual ICollection<EventPending> EventPendings { get; set; } = new List<EventPending>();

    [InverseProperty("Affiliate")]
    public virtual ICollection<SetMerchantAffiliate> SetMerchantAffiliates { get; set; } = new List<SetMerchantAffiliate>();

    [InverseProperty("AfsAffiliate")]
    public virtual ICollection<TblAffiliateFeeStep> TblAffiliateFeeSteps { get; set; } = new List<TblAffiliateFeeStep>();

    [InverseProperty("AfpAffiliate")]
    public virtual ICollection<TblAffiliatePayment> TblAffiliatePayments { get; set; } = new List<TblAffiliatePayment>();

    [InverseProperty("Afcaff")]
    public virtual ICollection<TblAffiliatesCount> TblAffiliatesCounts { get; set; } = new List<TblAffiliatesCount>();

    [InverseProperty("Affiliate")]
    public virtual ICollection<TblWireMoney> TblWireMoneys { get; set; } = new List<TblWireMoney>();

    [ForeignKey("AffiliatesId")]
    [InverseProperty("Affiliates")]
    public virtual ICollection<ApplicationIdentity> ApplicationIdentities { get; set; } = new List<ApplicationIdentity>();
}
