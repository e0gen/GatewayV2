namespace Ezzygate.Domain.Models;

public class Merchant
{
    public int Id { get; set; }
    public string CustomerNumber { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? HashKey { get; set; }
    public int? AccountId { get; set; }
    public bool IsActive { get; set; }
    public bool IsSendUserConfirmationEmail { get; set; }
    public bool IsMerchantNotifiedOnPass { get; set; }
    public bool IsMerchantNotifiedOnFail { get; set; }

    public Account? Account { get; set; }
}