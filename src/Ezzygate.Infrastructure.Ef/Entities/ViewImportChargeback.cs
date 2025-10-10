using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Keyless]
public partial class ViewImportChargeback
{
    [Column("CASE ID")]
    [StringLength(30)]
    [Unicode(false)]
    public string CaseId { get; set; } = null!;

    [Column("RSN")]
    [StringLength(30)]
    [Unicode(false)]
    public string Rsn { get; set; } = null!;

    [Column("MER-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string MerNo { get; set; } = null!;

    [Column("CARDHOLDER-NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string CardholderNumber { get; set; } = null!;

    [Column("TXN-DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string TxnDate { get; set; } = null!;

    [Column("AMOUNT-TXN")]
    [StringLength(30)]
    [Unicode(false)]
    public string AmountTxn { get; set; } = null!;

    [Column("CUR")]
    [StringLength(30)]
    [Unicode(false)]
    public string Cur { get; set; } = null!;

    [Column("AMOUNT-EURO")]
    [StringLength(30)]
    [Unicode(false)]
    public string AmountEuro { get; set; } = null!;

    [Column("REF-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string RefNo { get; set; } = null!;

    [Column("TICKET-NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string TicketNo { get; set; } = null!;

    [Column("DEADLINE")]
    [StringLength(30)]
    [Unicode(false)]
    public string Deadline { get; set; } = null!;

    [Column("COMMENTS")]
    [StringLength(30)]
    [Unicode(false)]
    public string Comments { get; set; } = null!;

    [Column("MRF")]
    [StringLength(30)]
    [Unicode(false)]
    public string Mrf { get; set; } = null!;

    [Column("POS_BRAND_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string PosBrandCode { get; set; } = null!;

    [Column("MERCHANT_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string MerchantNumber { get; set; } = null!;

    [Column("OUTLET_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string OutletNumber { get; set; } = null!;

    [Column("CHARGING_STATUS")]
    [StringLength(30)]
    [Unicode(false)]
    public string ChargingStatus { get; set; } = null!;

    [Column("MICROFILM_REF_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string MicrofilmRefNumber { get; set; } = null!;

    [Column("CARD_NUMBER")]
    [StringLength(30)]
    [Unicode(false)]
    public string CardNumber { get; set; } = null!;

    [Column("NETWORK_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string NetworkCode { get; set; } = null!;

    [Column("ACRONYM")]
    [StringLength(30)]
    [Unicode(false)]
    public string Acronym { get; set; } = null!;

    [Column("AMOUNT")]
    [StringLength(30)]
    [Unicode(false)]
    public string Amount { get; set; } = null!;

    [Column("CURRENCY_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string CurrencyCode { get; set; } = null!;

    [Column("SOURCE_AMOUNT")]
    [StringLength(30)]
    [Unicode(false)]
    public string SourceAmount { get; set; } = null!;

    [Column("SOURCE_CURRENCY_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string SourceCurrencyCode { get; set; } = null!;

    [Column("REASON_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ReasonCode { get; set; } = null!;

    [Column("TRANSACTION_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string TransactionDate { get; set; } = null!;

    [Column("PROCESSING_DATE")]
    [StringLength(30)]
    [Unicode(false)]
    public string ProcessingDate { get; set; } = null!;

    [Column("AUTHORIZATION_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string AuthorizationCode { get; set; } = null!;

    [Column("TRN_ID")]
    [StringLength(30)]
    [Unicode(false)]
    public string TrnId { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string Reason { get; set; } = null!;

    [StringLength(100)]
    public string? CurrencyIsoName { get; set; }

    [Column("TransactionDate", TypeName = "datetime")]
    public DateTime? TransactionDate1 { get; set; }

    public int FileDebitCompany { get; set; }

    [StringLength(50)]
    public string? FileDebitCompanyName { get; set; }

    [StringLength(25)]
    public string? CardNumberPartial { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? TransList { get; set; }

    [StringLength(300)]
    public string? MerchantList { get; set; }

    public byte PaymentMethod { get; set; }

    [StringLength(80)]
    public string? PaymentMethodName { get; set; }

    [Column("CountryISO")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryIso { get; set; } = null!;

    [StringLength(50)]
    public string CountryName { get; set; } = null!;

    [Column("ID")]
    public int Id { get; set; }

    [Column("icb_IsProcessed")]
    public bool IcbIsProcessed { get; set; }

    [Column("icb_User")]
    [StringLength(50)]
    [Unicode(false)]
    public string IcbUser { get; set; } = null!;

    [Column("icb_FileName")]
    [StringLength(100)]
    [Unicode(false)]
    public string IcbFileName { get; set; } = null!;

    [Column("icb_ChargebackDate", TypeName = "datetime")]
    public DateTime IcbChargebackDate { get; set; }

    [Column("icb_IsPhotocopy")]
    public int IcbIsPhotocopy { get; set; }
}
