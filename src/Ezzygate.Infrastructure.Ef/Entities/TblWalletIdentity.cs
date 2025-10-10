using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ezzygate.Infrastructure.Ef.Entities;

[Table("tblWalletIdentity")]
public partial class TblWalletIdentity
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("wi_Name")]
    [StringLength(50)]
    public string WiName { get; set; } = null!;

    [Column("wi_Identity")]
    [StringLength(50)]
    public string WiIdentity { get; set; } = null!;

    [Column("wi_Folder")]
    [StringLength(50)]
    public string WiFolder { get; set; } = null!;

    [Column("wi_BrandName")]
    [StringLength(50)]
    public string WiBrandName { get; set; } = null!;

    [Column("wi_CompanyName")]
    [StringLength(50)]
    public string WiCompanyName { get; set; } = null!;

    [Column("wi_DevCenterURL")]
    [StringLength(50)]
    public string WiDevCenterUrl { get; set; } = null!;

    [Column("wi_ProcessURL")]
    [StringLength(50)]
    public string WiProcessUrl { get; set; } = null!;

    [Column("wi_MerchantURL")]
    [StringLength(50)]
    public string WiMerchantUrl { get; set; } = null!;

    [Column("wi_CustomerURL")]
    [StringLength(50)]
    public string WiCustomerUrl { get; set; } = null!;

    [Column("wi_ContentURL")]
    [StringLength(50)]
    public string WiContentUrl { get; set; } = null!;

    [Column("wi_MerchantUploadsFolder")]
    [StringLength(100)]
    public string WiMerchantUploadsFolder { get; set; } = null!;

    [Column("wi_MailServer")]
    [StringLength(50)]
    public string WiMailServer { get; set; } = null!;

    [Column("wi_MailUsername")]
    [StringLength(50)]
    public string WiMailUsername { get; set; } = null!;

    [Column("wi_MailPassword")]
    [StringLength(50)]
    public string WiMailPassword { get; set; } = null!;

    [Column("wi_MailFrom")]
    [StringLength(50)]
    public string WiMailFrom { get; set; } = null!;

    [Column("wi_Currencies")]
    [StringLength(50)]
    public string WiCurrencies { get; set; } = null!;

    [Column("wi_FormsInbox")]
    [StringLength(150)]
    public string WiFormsInbox { get; set; } = null!;

    [Column("wi_DatabaseNumber")]
    public int WiDatabaseNumber { get; set; }

    [Column("wi_MerchantNumber")]
    public int WiMerchantNumber { get; set; }

    [Column("wi_IsHebrewVisible")]
    public bool WiIsHebrewVisible { get; set; }

    [Column("wi_IsSendMerchant")]
    public bool WiIsSendMerchant { get; set; }

    [Column("wi_IsSendCustomer")]
    public bool WiIsSendCustomer { get; set; }

    [Column("wi_IsDirectEbanking")]
    public bool WiIsDirectEbanking { get; set; }

    [Column("wi_ContentFolder")]
    [StringLength(20)]
    [Unicode(false)]
    public string WiContentFolder { get; set; } = null!;

    [Column("wi_MailTo")]
    [StringLength(50)]
    public string? WiMailTo { get; set; }

    [Column("wi_CopyRight")]
    [StringLength(250)]
    public string WiCopyRight { get; set; } = null!;

    [Column("wi_IsHandledByMerchant")]
    public bool WiIsHandledByMerchant { get; set; }

    [Column("wi_IsContactTransFields")]
    public bool WiIsContactTransFields { get; set; }

    [Column("wi_isEnable")]
    public bool WiIsEnable { get; set; }
}
