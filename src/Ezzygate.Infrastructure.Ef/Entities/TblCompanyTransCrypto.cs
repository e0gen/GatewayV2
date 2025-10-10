using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

/// <summary>
/// Transactions Crypto
/// </summary>
[Table("tblCompanyTransCrypto")]
public partial class TblCompanyTransCrypto
{
    [Key]
    [Column("tblCompanyTransCrypto_id")]
    public int TblCompanyTransCryptoId { get; set; }

    [Column("CompanyID")]
    public int CompanyId { get; set; }

    [StringLength(40)]
    [Unicode(false)]
    public string? TrxId { get; set; }

    public int TrxConfirmation { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? TrxStatus { get; set; }

    [StringLength(70)]
    public string? TrxHash { get; set; }

    [MaxLength(100)]
    public byte[]? WalletPrivateKey { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? WalletPublicKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? WalletAddress { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal WalletBalanceConfirmed { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal WalletBalanceUnconfirmed { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal WalletBalanceTotal { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? OriginalCurrencyIsoCode { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? ConvertedCurrencyIsoCode { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal OriginalAmount { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal ConvertedAmount { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal ConvertedAmountFee { get; set; }

    [Column(TypeName = "decimal(19, 10)")]
    public decimal ConvertedAmountMinerFee { get; set; }

    [Column("TransPayerInfo_id")]
    public int? TransPayerInfoId { get; set; }

    [Column("TransPaymentBillingAddress_id")]
    public int? TransPaymentBillingAddressId { get; set; }

    public DateTime TimeCreated { get; set; }

    [ForeignKey("CompanyId")]
    [InverseProperty("TblCompanyTransCryptos")]
    public virtual TblCompany Company { get; set; } = null!;

    [ForeignKey("ConvertedCurrencyIsoCode")]
    [InverseProperty("TblCompanyTransCryptoConvertedCurrencyIsoCodeNavigations")]
    public virtual CurrencyList? ConvertedCurrencyIsoCodeNavigation { get; set; }

    [ForeignKey("OriginalCurrencyIsoCode")]
    [InverseProperty("TblCompanyTransCryptoOriginalCurrencyIsoCodeNavigations")]
    public virtual CurrencyList? OriginalCurrencyIsoCodeNavigation { get; set; }

    [ForeignKey("TransPayerInfoId")]
    [InverseProperty("TblCompanyTransCryptos")]
    public virtual TransPayerInfo? TransPayerInfo { get; set; }

    [ForeignKey("TransPaymentBillingAddressId")]
    [InverseProperty("TblCompanyTransCryptos")]
    public virtual TransPaymentBillingAddress? TransPaymentBillingAddress { get; set; }
}
