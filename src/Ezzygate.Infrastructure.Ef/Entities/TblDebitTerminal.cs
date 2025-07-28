using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblDebitTerminals")]
[Index("DebitCompany", "TerminalNumber", Name = "IX_Company_Terminal")]
[Index("DtBankAccountId", Name = "ix_tblDebitTerminals_dt_BankAccountID")]
public partial class TblDebitTerminal
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public byte DebitCompany { get; set; }

    [Column("terminalNumber")]
    [StringLength(20)]
    public string TerminalNumber { get; set; } = null!;

    [Column("accountId")]
    [StringLength(50)]
    public string AccountId { get; set; } = null!;

    [Column("accountSubId")]
    [StringLength(50)]
    public string AccountSubId { get; set; } = null!;

    [Column("terminalNumber3D")]
    [StringLength(20)]
    public string TerminalNumber3D { get; set; } = null!;

    [Column("accountId3D")]
    [StringLength(50)]
    public string AccountId3D { get; set; } = null!;

    [Column("accountSubId3D")]
    [StringLength(50)]
    public string AccountSubId3D { get; set; } = null!;

    [Column("isEzzygateTerminal")]
    public bool IsEzzygateTerminal { get; set; }

    [Column("isShvaMasterTerminal")]
    public bool IsShvaMasterTerminal { get; set; }

    [Column("processingMethod")]
    public byte ProcessingMethod { get; set; }

    [Column("terminalMonthlyCost", TypeName = "money")]
    public decimal TerminalMonthlyCost { get; set; }

    [Column("terminalNotes")]
    [StringLength(3000)]
    public string TerminalNotes { get; set; } = null!;

    [Column("dt_CompanyNum_visa")]
    [StringLength(50)]
    public string DtCompanyNumVisa { get; set; } = null!;

    [Column("dt_Comments_visa")]
    [StringLength(50)]
    public string DtCommentsVisa { get; set; } = null!;

    [Column("dt_CompanyNum_isracard")]
    [StringLength(50)]
    public string DtCompanyNumIsracard { get; set; } = null!;

    [Column("dt_Comments_isracard")]
    [StringLength(50)]
    public string DtCommentsIsracard { get; set; } = null!;

    [Column("dt_CompanyNum_direct")]
    [StringLength(50)]
    public string DtCompanyNumDirect { get; set; } = null!;

    [Column("dt_Comments_direct")]
    [StringLength(50)]
    public string DtCommentsDirect { get; set; } = null!;

    [Column("dt_CompanyNum_mastercard")]
    [StringLength(50)]
    public string DtCompanyNumMastercard { get; set; } = null!;

    [Column("dt_Comments_mastercard")]
    [StringLength(50)]
    public string DtCommentsMastercard { get; set; } = null!;

    [Column("dt_CompanyNum_diners")]
    [StringLength(50)]
    public string DtCompanyNumDiners { get; set; } = null!;

    [Column("dt_Comments_diners")]
    [StringLength(50)]
    public string DtCommentsDiners { get; set; } = null!;

    [Column("dt_CompanyNum_americanexp")]
    [StringLength(50)]
    public string DtCompanyNumAmericanexp { get; set; } = null!;

    [Column("dt_Comments_americanexp")]
    [StringLength(500)]
    public string DtCommentsAmericanexp { get; set; } = null!;

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("accountPassword256")]
    [MaxLength(1500)]
    public byte[]? AccountPassword256 { get; set; }

    [Column("accountPassword3D256")]
    [MaxLength(1500)]
    public byte[]? AccountPassword3D256 { get; set; }

    [Column("dt_mcc")]
    [StringLength(15)]
    [Unicode(false)]
    public string DtMcc { get; set; } = null!;

    [Column("dt_name")]
    [StringLength(80)]
    public string DtName { get; set; } = null!;

    [Column("dt_isManipulateAmount")]
    public bool DtIsManipulateAmount { get; set; }

    [Column("dt_IsUseBlackList")]
    public bool DtIsUseBlackList { get; set; }

    [Column("dt_Descriptor")]
    [StringLength(50)]
    public string? DtDescriptor { get; set; }

    [Column("dt_MonthlyLimitCHB")]
    public int DtMonthlyLimitChb { get; set; }

    [Column("dt_MonthlyCHB")]
    public int DtMonthlyChb { get; set; }

    [Column("dt_MonthlyCHBWasSent")]
    public bool DtMonthlyChbwasSent { get; set; }

    [Column("dt_MonthlyCHBSendDate", TypeName = "datetime")]
    public DateTime DtMonthlyChbsendDate { get; set; }

    [Column("dt_MonthlyMCLimitCHB")]
    public int DtMonthlyMclimitChb { get; set; }

    [Column("dt_MonthlyMCCHB")]
    public int DtMonthlyMcchb { get; set; }

    [Column("dt_MonthlyMCCHBWasSent")]
    public bool DtMonthlyMcchbwasSent { get; set; }

    [Column("dt_MonthlyMCCHBSendDate", TypeName = "datetime")]
    public DateTime DtMonthlyMcchbsendDate { get; set; }

    [Column("dt_BankAccountID")]
    public int DtBankAccountId { get; set; }

    [Column("dt_IsRefundBlocked")]
    public bool DtIsRefundBlocked { get; set; }

    [Column("dt_MonthlyLimitCHBNotifyUsers")]
    [StringLength(100)]
    public string DtMonthlyLimitChbnotifyUsers { get; set; } = null!;

    [Column("dt_MonthlyLimitCHBNotifyUsersSMS")]
    [StringLength(100)]
    public string DtMonthlyLimitChbnotifyUsersSms { get; set; } = null!;

    [Column("dt_MonthlyMCLimitCHBNotifyUsers")]
    [StringLength(100)]
    public string DtMonthlyMclimitChbnotifyUsers { get; set; } = null!;

    [Column("dt_MonthlyMCLimitCHBNotifyUsersSMS")]
    [StringLength(100)]
    public string DtMonthlyMclimitChbnotifyUsersSms { get; set; } = null!;

    [Column("dt_ContractNumber")]
    [StringLength(50)]
    public string DtContractNumber { get; set; } = null!;

    [Column("dt_ManagingCompany")]
    public int? DtManagingCompany { get; set; }

    [Column("dt_ManagingPSP")]
    public int? DtManagingPsp { get; set; }

    [Column("dt_EnableRecurringBank")]
    public bool DtEnableRecurringBank { get; set; }

    [Column("dt_EnableAuthorization")]
    public bool DtEnableAuthorization { get; set; }

    [Column("dt_Enable3dsecure")]
    public bool DtEnable3dsecure { get; set; }

    [Column("dt_mobileCat")]
    public int? DtMobileCat { get; set; }

    [Column("dt_SMSShortCode")]
    [StringLength(15)]
    public string? DtSmsshortCode { get; set; }

    [Column("dt_IsPersonalNumberRequired")]
    public bool DtIsPersonalNumberRequired { get; set; }

    [Column("dt_IsTestTerminal")]
    public bool DtIsTestTerminal { get; set; }

    [Column("dt_BankExternalID")]
    [StringLength(30)]
    public string? DtBankExternalId { get; set; }

    [StringLength(1500)]
    [Unicode(false)]
    public string? AuthenticationCode1 { get; set; }

    [StringLength(1500)]
    [Unicode(false)]
    public string? AuthenticationCode3D { get; set; }

    [Column("SICCodeNumber")]
    public short? SiccodeNumber { get; set; }

    [StringLength(20)]
    public string? SearchTag { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string? BlacklistCountry { get; set; }

    [ForeignKey("SiccodeNumber")]
    [InverseProperty("TblDebitTerminals")]
    public virtual Siccode? SiccodeNumberNavigation { get; set; }
}
