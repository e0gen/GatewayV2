using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitCompany")]
public partial class TblDebitCompany
{
    [Key]
    [Column("DebitCompany_ID")]
    public int DebitCompanyId { get; set; }

    [Column("dc_name")]
    [StringLength(50)]
    public string DcName { get; set; } = null!;

    [Column("dc_description")]
    [StringLength(1000)]
    public string DcDescription { get; set; } = null!;

    [Column("dc_isAllowRefund")]
    public bool DcIsAllowRefund { get; set; }

    [Column("dc_isAllowPartialRefund")]
    public bool DcIsAllowPartialRefund { get; set; }

    [Column("dc_isReturnCode")]
    public bool DcIsReturnCode { get; set; }

    [Column("dc_isActive")]
    public bool DcIsActive { get; set; }

    [Column("dc_nextTransID")]
    public int DcNextTransId { get; set; }

    [Column("dc_TempBlocks")]
    public int DcTempBlocks { get; set; }

    [Column("dc_UnblockAttemptString")]
    [StringLength(2000)]
    public string DcUnblockAttemptString { get; set; } = null!;

    [Column("dc_IsBlocked")]
    public bool DcIsBlocked { get; set; }

    [Column("dc_EmergencyPhone")]
    [StringLength(50)]
    [Unicode(false)]
    public string DcEmergencyPhone { get; set; } = null!;

    [Column("dc_EmergencyContact")]
    [StringLength(50)]
    [Unicode(false)]
    public string DcEmergencyContact { get; set; } = null!;

    [Column("dc_MonthlyLimitCHB")]
    public int DcMonthlyLimitChb { get; set; }

    [Column("dc_MonthlyMCLimitCHB")]
    public int DcMonthlyMclimitChb { get; set; }

    [Column("dc_MonthlyLimitCHBNotifyUsers")]
    [StringLength(100)]
    public string DcMonthlyLimitChbnotifyUsers { get; set; } = null!;

    [Column("dc_MonthlyLimitCHBNotifyUsersSMS")]
    [StringLength(100)]
    public string DcMonthlyLimitChbnotifyUsersSms { get; set; } = null!;

    [Column("dc_MonthlyMCLimitCHBNotifyUsers")]
    [StringLength(100)]
    public string DcMonthlyMclimitChbnotifyUsers { get; set; } = null!;

    [Column("dc_MonthlyMCLimitCHBNotifyUsersSMS")]
    [StringLength(100)]
    public string DcMonthlyMclimitChbnotifyUsersSms { get; set; } = null!;

    [Column("dc_AutoRefundNotifyUsers")]
    [StringLength(100)]
    public string DcAutoRefundNotifyUsers { get; set; } = null!;

    [Column("dc_IsAutoRefund")]
    public bool DcIsAutoRefund { get; set; }

    [Column("dc_IsAllowPayoutWithoutTransfer")]
    public bool DcIsAllowPayoutWithoutTransfer { get; set; }

    [Column("dc_IsNotifyRetReqAfterRefund")]
    public bool? DcIsNotifyRetReqAfterRefund { get; set; }

    [StringLength(50)]
    public string? RegistrationUsrname { get; set; }

    [MaxLength(50)]
    public byte[]? RegistrationPassword256 { get; set; }

    public bool? IsDynamicDescriptor { get; set; }

    [Column("dc_isSupportPartialRefund")]
    public bool DcIsSupportPartialRefund { get; set; }

    public int? RetentionPeriodMinutes { get; set; }

    [Column("Account_id")]
    public int? AccountId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? IntegrationTag { get; set; }

    public bool? IsAllowCancelPreAuth { get; set; }

    [InverseProperty("DebitCompany")]
    public virtual Account? Account { get; set; }

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TblCompanyTransApproval> TblCompanyTransApprovals { get; set; } = new List<TblCompanyTransApproval>();

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TblCompanyTransFail> TblCompanyTransFails { get; set; } = new List<TblCompanyTransFail>();

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TblCompanyTransPass> TblCompanyTransPasses { get; set; } = new List<TblCompanyTransPass>();

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TblCompanyTransPending> TblCompanyTransPendings { get; set; } = new List<TblCompanyTransPending>();

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TblDebitCompanyPaymentTokenization> TblDebitCompanyPaymentTokenizations { get; set; } = new List<TblDebitCompanyPaymentTokenization>();

    [InverseProperty("LcaDebitCompany")]
    public virtual ICollection<TblLogChargeAttempt> TblLogChargeAttempts { get; set; } = new List<TblLogChargeAttempt>();

    [InverseProperty("SudcDebitCompanyNavigation")]
    public virtual ICollection<TblSecurityUserDebitCompany> TblSecurityUserDebitCompanies { get; set; } = new List<TblSecurityUserDebitCompany>();

    [InverseProperty("DebitCompany")]
    public virtual ICollection<TransMatchPending> TransMatchPendings { get; set; } = new List<TransMatchPending>();

    [ForeignKey("DebitCompanyId")]
    [InverseProperty("DebitCompanies")]
    public virtual ICollection<TransAmountType> TransAmountTypes { get; set; } = new List<TransAmountType>();
}
