using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewPaymentsInfo
{
    [Column("CompanyMakePaymentsRequests_id")]
    public int CompanyMakePaymentsRequestsId { get; set; }

    [Column("CompanyMakePaymentsProfiles_id")]
    public int? CompanyMakePaymentsProfilesId { get; set; }

    [Column("Company_id")]
    public int? CompanyId { get; set; }

    [Column("paymentType")]
    public byte PaymentType { get; set; }

    [Column("paymentDate", TypeName = "smalldatetime")]
    public DateTime PaymentDate { get; set; }

    [Column("paymentAmount")]
    public double PaymentAmount { get; set; }

    [Column("paymentCurrency")]
    public int? PaymentCurrency { get; set; }

    [Column("paymentExchangeRate")]
    public double PaymentExchangeRate { get; set; }

    [Column("paymentMerchantComment")]
    [StringLength(250)]
    public string PaymentMerchantComment { get; set; } = null!;

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

    [Column("bankIsraelInfo_BankCode")]
    public int BankIsraelInfoBankCode { get; set; }

    [Column("basicInfo_costumerName")]
    [StringLength(50)]
    public string? BasicInfoCostumerName { get; set; }

    [Column("bankAbroadAccountName")]
    [StringLength(80)]
    public string? BankAbroadAccountName { get; set; }

    [Column("bankAbroadAccountName2")]
    [StringLength(80)]
    public string? BankAbroadAccountName2 { get; set; }

    [Column("basicInfo_costumerNumber")]
    [StringLength(10)]
    public string? BasicInfoCostumerNumber { get; set; }

    public byte? WireStatus { get; set; }

    [Column(TypeName = "smalldatetime")]
    public DateTime? WireStatusDate { get; set; }

    [StringLength(1000)]
    public string? WireComment { get; set; }

    [Column("WireMoney_id")]
    public int? WireMoneyId { get; set; }

    [StringLength(100)]
    public string? WireFileName { get; set; }
}
