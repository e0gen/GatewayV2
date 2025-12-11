using System.Text.Json.Serialization;

namespace Ezzygate.Application.Configuration;

public sealed class DomainConfiguration
{
    // Basic Information
    public string Host { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public string ThemeFolder { get; set; } = string.Empty;
    public string GoogleAnalytics { get; set; } = string.Empty;

    // Paths and URLs
    public string DataPath { get; set; } = string.Empty;
    public string PublicDataVirtualPath { get; set; } = string.Empty;
    public string CommonVirtualPath { get; set; } = "/NPCommon/";
    public string CommonPhysicalPath { get; set; } = string.Empty;

    // Security
    public int EncryptionKeyNumber { get; set; }
    public string EzzygateAdminsNTGroupName { get; set; } = string.Empty;
    public string ServiceUser { get; set; } = string.Empty;
    public string ServiceUserPassword { get; set; } = string.Empty;
    public string WebApisMobileSecurityKey { get; set; } = string.Empty;
    public string WebApisMobileManualSecurityKey { get; set; } = string.Empty;
    public string WebApisMobileJwtSecretKey { get; set; } = string.Empty;

    // Features and Flags
    public bool EnableEpa { get; set; }

    [JsonPropertyName("ForceSSL")]
    public bool ForceSsl { get; set; }

    public bool IsHebrewVisible { get; set; }
    public bool SaveCui { get; set; }
    public bool UsePublicPage { get; set; }
    public bool MultiFactorAdmin { get; set; }
    public bool DisablePostRedirectUrl { get; set; } = false;

    // Database Connections
    public string Sql1ConnectionString { get; set; } = string.Empty;
    public string Sql2ConnectionString { get; set; } = string.Empty;
    public string ReportsConnectionString { get; set; } = string.Empty;
    public string SqlArchiveConnectionString { get; set; } = string.Empty;
    public string AnalysisServicesConnectionString { get; set; } = string.Empty;
    public string IntegrationServicesReportsConnectionString { get; set; } = string.Empty;

    [JsonPropertyName("CRMConnectionString")]
    public string CrmConnectionString { get; set; } = string.Empty;

    // Email Settings
    public string MailNameFrom { get; set; } = string.Empty;
    public string MailAddressFrom { get; set; } = string.Empty;
    public string MailNameTo { get; set; } = string.Empty;
    public string MailAddressTo { get; set; } = string.Empty;
    public string MailAddressFromChb { get; set; } = string.Empty;
    public string SmtpHost { get; set; } = string.Empty;
    public string SmtpPort { get; set; } = string.Empty;
    public string SmtpUserName { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;

    // SMS Settings
    public string SmsServiceProvider { get; set; } = string.Empty;
    public string SmsServiceUserName { get; set; } = string.Empty;
    public string SmsServicePassword { get; set; } = string.Empty;
    public string SmsServiceFrom { get; set; } = string.Empty;

    // Application URLs
    public string MerchantUrl { get; set; } = string.Empty;
    public string ReportsUrl { get; set; } = string.Empty;
    public string DevCenterUrl { get; set; } = string.Empty;
    public string ProcessUrl { get; set; } = string.Empty;
    public string ProcessV2Url { get; set; } = string.Empty;
    public string CartUrl { get; set; } = string.Empty;
    public string WalletUrl { get; set; } = string.Empty;
    public string WebServicesUrl { get; set; } = string.Empty;
    public string WebApiUrl { get; set; } = string.Empty;
    public string PartnersUrl { get; set; } = string.Empty;
    public string ShopUrl { get; set; } = string.Empty;
    public string MiniVTUrl { get; set; } = string.Empty;
    public string AdminUrl { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string SignupUrl { get; set; } = string.Empty;

    // Social Media
    public string SocialMediaFacebook { get; set; } = string.Empty;
    public string SocialMediaYoutube { get; set; } = string.Empty;
    public string SocialMediaLinkedIn { get; set; } = string.Empty;
    public string SocialMediaTwitter { get; set; } = string.Empty;

    // Support Information
    public string CustomerServiceEmail { get; set; } = string.Empty;
    public string CustomerServiceSkype { get; set; } = string.Empty;
    public string CustomerServiceFax { get; set; } = string.Empty;
    public string CustomerServicePhone { get; set; } = string.Empty;
    public string CustomerServiceUSPhone { get; set; } = string.Empty;
    public string TechnicalSupportEmail { get; set; } = string.Empty;
    public string TechnicalSupportPhone { get; set; } = string.Empty;
    public string TechnicalSupportFax { get; set; } = string.Empty;
    public string TechnicalSupportSkype { get; set; } = string.Empty;

    // Company Information
    public string IdentityAddress { get; set; } = string.Empty;
    public string IdentityCity { get; set; } = string.Empty;
    public string IdentityZipcode { get; set; } = string.Empty;
    public string IdentityCountry { get; set; } = string.Empty;

    // CRM
    [JsonPropertyName("CRMEmailTemplatePath")]
    public string CrmEmailTemplatePath { get; set; } = string.Empty;

    // Finance
    public string FinanceLevel1Group { get; set; } = string.Empty;
    public string FinanceLevel2Group { get; set; } = string.Empty;

    // Monitoring & Fraud Detection
    public string FraudDetectionAlertRecipients { get; set; } = string.Empty;
    public string FraudDetectionOperationMode { get; set; } = string.Empty;
    public string ActiveMerchantReportRecipients { get; set; } = string.Empty;
    public string BnsTransactionsWithoutEpaReportRecipients { get; set; } = string.Empty;
    public string IrregularityReportRecipients { get; set; } = string.Empty;
    public string DetectPhotocopyWithRefundRecipients { get; set; } = string.Empty;
    public string SettlementWalletAddress { get; set; } = string.Empty;

    // External Services
    public string FreshdeskApiKey { get; set; } = string.Empty;
}