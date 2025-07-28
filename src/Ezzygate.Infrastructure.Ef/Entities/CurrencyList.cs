using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("CurrencyList", Schema = "List")]
public partial class CurrencyList
{
    [Key]
    [Column("CurrencyISOCode")]
    [StringLength(3)]
    [Unicode(false)]
    public string CurrencyIsocode { get; set; } = null!;

    [Column("ISONumber")]
    [StringLength(3)]
    [Unicode(false)]
    public string Isonumber { get; set; } = null!;

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(25)]
    public string? Symbol { get; set; }

    [Column(TypeName = "decimal(10, 4)")]
    public decimal? BaseRate { get; set; }

    [Column(TypeName = "decimal(6, 4)")]
    public decimal? ExchangeFeeInd { get; set; }

    [Precision(0)]
    public DateTime? RateRequestDate { get; set; }

    [Precision(0)]
    public DateTime? RateValueDate { get; set; }

    [Column(TypeName = "money")]
    public decimal? MaxTransactionAmount { get; set; }

    public bool? IsSymbolBeforeAmount { get; set; }

    [Precision(0)]
    public DateTime? InsertDate { get; set; }

    [Column("CurrencyID")]
    public int? CurrencyId { get; set; }

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<AccountBalanceMoneyRequest> AccountBalanceMoneyRequests { get; set; } = new List<AccountBalanceMoneyRequest>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<AccountBankAccount> AccountBankAccounts { get; set; } = new List<AccountBankAccount>();

    [InverseProperty("DefaultCurrencyIsocodeNavigation")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<ApplicationIdentityToCurrency> ApplicationIdentityToCurrencies { get; set; } = new List<ApplicationIdentityToCurrency>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<CurrencyRate> CurrencyRates { get; set; } = new List<CurrencyRate>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<MerchantSetShop> MerchantSetShops { get; set; } = new List<MerchantSetShop>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<PeriodicFeeType> PeriodicFeeTypes { get; set; } = new List<PeriodicFeeType>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<SetMerchantPeriodicFee> SetMerchantPeriodicFees { get; set; } = new List<SetMerchantPeriodicFee>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<SetPeriodicFee> SetPeriodicFees { get; set; } = new List<SetPeriodicFee>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<SetTransactionFee> SetTransactionFees { get; set; } = new List<SetTransactionFee>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<SetTransactionFloor> SetTransactionFloors { get; set; } = new List<SetTransactionFloor>();

    [InverseProperty("ConvertedCurrencyIsoCodeNavigation")]
    public virtual ICollection<TblCompanyTransCrypto> TblCompanyTransCryptoConvertedCurrencyIsoCodeNavigations { get; set; } = new List<TblCompanyTransCrypto>();

    [InverseProperty("OriginalCurrencyIsoCodeNavigation")]
    public virtual ICollection<TblCompanyTransCrypto> TblCompanyTransCryptoOriginalCurrencyIsoCodeNavigations { get; set; } = new List<TblCompanyTransCrypto>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<TransMatchPending> TransMatchPendings { get; set; } = new List<TransMatchPending>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<TransactionAmount> TransactionAmounts { get; set; } = new List<TransactionAmount>();

    [InverseProperty("BatchCurrencyIsocodeNavigation")]
    public virtual ICollection<WireBatch> WireBatches { get; set; } = new List<WireBatch>();

    [InverseProperty("CurrencyIsocodeNavigation")]
    public virtual ICollection<Wire> WireCurrencyIsocodeNavigations { get; set; } = new List<Wire>();

    [InverseProperty("CurrencyIsocodeProcessedNavigation")]
    public virtual ICollection<Wire> WireCurrencyIsocodeProcessedNavigations { get; set; } = new List<Wire>();
}
