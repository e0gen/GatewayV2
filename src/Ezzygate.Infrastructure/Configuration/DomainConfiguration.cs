namespace Ezzygate.Infrastructure.Configuration;

public sealed class DomainConfiguration
{
    public string Host { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string ThemeFolder { get; set; } = string.Empty;
    public string DataPath { get; set; } = string.Empty;
    public string PublicDataVirtualPath { get; set; } = string.Empty;
    public bool MultiFactorAdmin { get; set; }
    public string FraudDetectionAlertRecipients { get; set; } = string.Empty;
    public string FraudDetectionOperationMode { get; set; } = "NoActions";
    public string ActiveMerchantReportRecipients { get; set; } = string.Empty;
    public string BnsTransactionsWithoutEpaReportRecipients { get; set; } = string.Empty;
    public string IrregularityReportRecipients { get; set; } = string.Empty;
    public string DetectPhotocopyWithRefundRecipients { get; set; } = string.Empty;
}