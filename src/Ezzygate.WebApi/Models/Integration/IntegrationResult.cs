namespace Ezzygate.WebApi.Models.Integration;

public class IntegrationResult
{
    public string? Code { get; set; }
    public string? Message { get; set; }
    public string? RedirectUrl { get; set; }
    public string? ApprovalNumber { get; set; }
    public string? DebitRefCode { get; set; }
    public string? DebitRefNum { get; set; }
    public string? TerminalNumber { get; set; }
    public int? TrxId { get; set; }
    public int? DebitCompanyId { get; set; }
    public bool IsFinalized { get; set; }
    public int? TrxType { get; set; }
    public int? RecurringId { get; set; }
    public int? CardStorageId { get; set; }
    public string? NotificationResponse { get; set; }
    public string? BinCountry { get; set; }
    public string? StatusApiUrl { get; set; }
    public string? StatusApiSignature { get; set; }
    public dynamic? CustomParams { get; set; }
}
