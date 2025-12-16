using Microsoft.Extensions.Configuration;
using Ezzygate.Application.Configuration;

namespace Ezzygate.Infrastructure.Configuration.Xml;

public static class XmlConfigurationExtensions
{
    private const string XmlInfrastructureXmlConfigFile = "Ezzygate.Infrastructure.config.xml";

    public static IConfigurationBuilder AddXmlInfrastructureConfigurationSource(
        this IConfigurationBuilder builder,
        string? basePath = null)
    {
        var xmlConfigPath = ResolveConfigPath(XmlInfrastructureXmlConfigFile, basePath);

        if (File.Exists(xmlConfigPath))
            return builder.AddXmlInfrastructureConfiguration(xmlConfigPath);

        throw new FileNotFoundException($"Infrastructure configuration file not found: {xmlConfigPath}");
    }

    private static string ResolveConfigPath(string fileName, string? basePath)
    {
        var configPath = string.IsNullOrEmpty(basePath) ? fileName : Path.Combine(basePath, fileName);

        if (!string.IsNullOrEmpty(basePath) || File.Exists(configPath))
            return Path.GetFullPath(configPath);

        var appBasePath = AppContext.BaseDirectory;
        var appConfigPath = Path.Combine(appBasePath, fileName);

        return File.Exists(appConfigPath) ? appConfigPath : Path.GetFullPath(configPath);
    }

    private static IConfigurationBuilder AddXmlInfrastructureConfiguration(
        this IConfigurationBuilder builder,
        string xmlConfigPath)
    {
        var appConfig = XmlConfigurationReader.ReadXmlConfiguration(xmlConfigPath);
        var configDict = ConvertToConfigurationDictionary(appConfig);
        builder.AddInMemoryCollection(configDict);
        return builder;
    }

    private static Dictionary<string, string?> ConvertToConfigurationDictionary(ApplicationConfiguration config)
    {
        var dict = new Dictionary<string, string?>();
        var section = ApplicationConfiguration.SectionName;

        dict[$"{section}:InstanceName"] = config.InstanceName;
        dict[$"{section}:IsProduction"] = config.IsProduction.ToString();
        dict[$"{section}:EnableClearCacheTimer"] = config.EnableClearCacheTimer.ToString();
        dict[$"{section}:NumericFormat"] = config.NumericFormat;
        dict[$"{section}:AmountFormat"] = config.AmountFormat;
        dict[$"{section}:SessionTimeout"] = config.SessionTimeout.ToString();
        dict[$"{section}:EnableTaskScheduler"] = config.EnableTaskScheduler.ToString();
        dict[$"{section}:EnableQueuedTasks"] = config.EnableQueuedTasks.ToString();
        dict[$"{section}:EnableEmail"] = config.EnableEmail.ToString();
        dict[$"{section}:SmtpHost"] = config.SmtpHost;
        dict[$"{section}:SmtpPort"] = config.SmtpPort.ToString();
        dict[$"{section}:SmtpUserName"] = config.SmtpUserName;
        dict[$"{section}:SmtpPassword"] = config.SmtpPassword;
        dict[$"{section}:MailAddressFrom"] = config.MailAddressFrom;
        dict[$"{section}:MaxImmediateReportSize"] = config.MaxImmediateReportSize.ToString();
        dict[$"{section}:MaxAdminFileManagerUploadSizeBytes"] = config.MaxAdminFileManagerUploadSizeBytes.ToString();
        dict[$"{section}:MaxFiledReportSize"] = config.MaxFiledReportSize.ToString();
        dict[$"{section}:PhysicalPath"] = config.PhysicalPath;
        dict[$"{section}:ApplicationPath"] = config.ApplicationPath;
        dict[$"{section}:CommonApplicationFiles"] = config.CommonApplicationFiles;
        dict[$"{section}:LogsPhysicalBasePath"] = config.LogsPhysicalBasePath;
        dict[$"{section}:SftpExecutablePath"] = config.SftpExecutablePath;
        dict[$"{section}:PgpExecutablePath"] = config.PgpExecutablePath;
        dict[$"{section}:ErrorNetConnectionString"] = config.ErrorNetConnectionString;
        dict[$"{section}:IsErrorNetConnectionStringEncrypted"] = config.IsErrorNetConnectionStringEncrypted.ToString();
        dict[$"{section}:SecurityProtocols"] = config.SecurityProtocols;
        dict[$"{section}:Version"] = config.Version;
        dict[$"{section}:FullVersion"] = config.FullVersion;

        for (var i = 0; i < config.Domains.Count; i++)
        {
            var domain = config.Domains[i];
            var domainSection = $"{section}:Domains:{i}";
            AddDomainToDictionary(dict, domainSection, domain);
        }

        for (var i = 0; i < config.ScheduledTasks.Count; i++)
        {
            var task = config.ScheduledTasks[i];
            var taskSection = $"{section}:ScheduledTasks:{i}";
            dict[$"{taskSection}:GroupId"] = task.GroupId;
            dict[$"{taskSection}:Description"] = task.Description;
            dict[$"{taskSection}:Tag"] = task.Tag;
            dict[$"{taskSection}:AssemblyName"] = task.AssemblyName;
            dict[$"{taskSection}:TypeName"] = task.TypeName;
            dict[$"{taskSection}:MethodName"] = task.MethodName;
            dict[$"{taskSection}:Args"] = task.Args;
            dict[$"{taskSection}:Schedule:Interval"] = task.Schedule.Interval;
            dict[$"{taskSection}:Schedule:Every"] = task.Schedule.Every.ToString();
            dict[$"{taskSection}:Schedule:Hour"] = task.Schedule.Hour?.ToString();
            dict[$"{taskSection}:Schedule:Minute"] = task.Schedule.Minute?.ToString();
            dict[$"{taskSection}:Schedule:DayOfWeek"] = task.Schedule.DayOfWeek;
            dict[$"{taskSection}:Schedule:DayOfMonth"] = task.Schedule.DayOfMonth?.ToString();
        }

        return dict;
    }

    private static void AddDomainToDictionary(Dictionary<string, string?> dict, string section, DomainConfiguration domain)
    {
        // Basic Information
        dict[$"{section}:Host"] = domain.Host;
        dict[$"{section}:LegalName"] = domain.LegalName;
        dict[$"{section}:BrandName"] = domain.BrandName;
        dict[$"{section}:ShortCode"] = domain.ShortCode;
        dict[$"{section}:ThemeFolder"] = domain.ThemeFolder;
        dict[$"{section}:GoogleAnalytics"] = domain.GoogleAnalytics;

        // Paths and URLs
        dict[$"{section}:DataPath"] = domain.DataPath;
        dict[$"{section}:PublicDataVirtualPath"] = domain.PublicDataVirtualPath;
        dict[$"{section}:CommonVirtualPath"] = domain.CommonVirtualPath;
        dict[$"{section}:CommonPhysicalPath"] = domain.CommonPhysicalPath;

        // Security
        dict[$"{section}:EncryptionKeyNumber"] = domain.EncryptionKeyNumber.ToString();
        dict[$"{section}:EzzygateAdminsNTGroupName"] = domain.EzzygateAdminsNTGroupName;
        dict[$"{section}:ServiceUser"] = domain.ServiceUser;
        dict[$"{section}:ServiceUserPassword"] = domain.ServiceUserPassword;
        dict[$"{section}:WebApisMobileSecurityKey"] = domain.WebApisMobileSecurityKey;
        dict[$"{section}:WebApisMobileManualSecurityKey"] = domain.WebApisMobileManualSecurityKey;
        dict[$"{section}:WebApisMobileJwtSecretKey"] = domain.WebApisMobileJwtSecretKey;

        // Features and Flags
        dict[$"{section}:EnableEpa"] = domain.EnableEpa.ToString();
        dict[$"{section}:ForceSsl"] = domain.ForceSsl.ToString();
        dict[$"{section}:IsHebrewVisible"] = domain.IsHebrewVisible.ToString();
        dict[$"{section}:SaveCui"] = domain.SaveCui.ToString();
        dict[$"{section}:UsePublicPage"] = domain.UsePublicPage.ToString();
        dict[$"{section}:MultiFactorAdmin"] = domain.MultiFactorAdmin.ToString();
        dict[$"{section}:DisablePostRedirectUrl"] = domain.DisablePostRedirectUrl.ToString();

        // Database Connections
        dict[$"{section}:Sql1ConnectionString"] = domain.Sql1ConnectionString;
        dict[$"{section}:Sql2ConnectionString"] = domain.Sql2ConnectionString;
        dict[$"{section}:ReportsConnectionString"] = domain.ReportsConnectionString;
        dict[$"{section}:SqlArchiveConnectionString"] = domain.SqlArchiveConnectionString;
        dict[$"{section}:AnalysisServicesConnectionString"] = domain.AnalysisServicesConnectionString;
        dict[$"{section}:IntegrationServicesReportsConnectionString"] = domain.IntegrationServicesReportsConnectionString;
        dict[$"{section}:CrmConnectionString"] = domain.CrmConnectionString;

        // Email Settings
        dict[$"{section}:MailNameFrom"] = domain.MailNameFrom;
        dict[$"{section}:MailAddressFrom"] = domain.MailAddressFrom;
        dict[$"{section}:MailNameTo"] = domain.MailNameTo;
        dict[$"{section}:MailAddressTo"] = domain.MailAddressTo;
        dict[$"{section}:MailAddressFromChb"] = domain.MailAddressFromChb;
        dict[$"{section}:SmtpHost"] = domain.SmtpHost;
        dict[$"{section}:SmtpPort"] = domain.SmtpPort;
        dict[$"{section}:SmtpUserName"] = domain.SmtpUserName;
        dict[$"{section}:SmtpPassword"] = domain.SmtpPassword;

        // SMS Settings
        dict[$"{section}:SmsServiceProvider"] = domain.SmsServiceProvider;
        dict[$"{section}:SmsServiceUserName"] = domain.SmsServiceUserName;
        dict[$"{section}:SmsServicePassword"] = domain.SmsServicePassword;
        dict[$"{section}:SmsServiceFrom"] = domain.SmsServiceFrom;

        // Application URLs
        dict[$"{section}:MerchantUrl"] = domain.MerchantUrl;
        dict[$"{section}:ReportsUrl"] = domain.ReportsUrl;
        dict[$"{section}:DevCenterUrl"] = domain.DevCenterUrl;
        dict[$"{section}:ProcessUrl"] = domain.ProcessUrl;
        dict[$"{section}:ProcessV2Url"] = domain.ProcessV2Url;
        dict[$"{section}:CartUrl"] = domain.CartUrl;
        dict[$"{section}:WalletUrl"] = domain.WalletUrl;
        dict[$"{section}:WebServicesUrl"] = domain.WebServicesUrl;
        dict[$"{section}:WebApiUrl"] = domain.WebApiUrl;
        dict[$"{section}:PartnersUrl"] = domain.PartnersUrl;
        dict[$"{section}:ShopUrl"] = domain.ShopUrl;
        dict[$"{section}:MiniVTUrl"] = domain.MiniVTUrl;
        dict[$"{section}:AdminUrl"] = domain.AdminUrl;
        dict[$"{section}:ContentUrl"] = domain.ContentUrl;
        dict[$"{section}:WebsiteUrl"] = domain.WebsiteUrl;
        dict[$"{section}:SignupUrl"] = domain.SignupUrl;

        // Social Media
        dict[$"{section}:SocialMediaFacebook"] = domain.SocialMediaFacebook;
        dict[$"{section}:SocialMediaYoutube"] = domain.SocialMediaYoutube;
        dict[$"{section}:SocialMediaLinkedIn"] = domain.SocialMediaLinkedIn;
        dict[$"{section}:SocialMediaTwitter"] = domain.SocialMediaTwitter;

        // Support Information
        dict[$"{section}:CustomerServiceEmail"] = domain.CustomerServiceEmail;
        dict[$"{section}:CustomerServiceSkype"] = domain.CustomerServiceSkype;
        dict[$"{section}:CustomerServiceFax"] = domain.CustomerServiceFax;
        dict[$"{section}:CustomerServicePhone"] = domain.CustomerServicePhone;
        dict[$"{section}:CustomerServiceUSPhone"] = domain.CustomerServiceUSPhone;
        dict[$"{section}:TechnicalSupportEmail"] = domain.TechnicalSupportEmail;
        dict[$"{section}:TechnicalSupportPhone"] = domain.TechnicalSupportPhone;
        dict[$"{section}:TechnicalSupportFax"] = domain.TechnicalSupportFax;
        dict[$"{section}:TechnicalSupportSkype"] = domain.TechnicalSupportSkype;

        // Company Information
        dict[$"{section}:IdentityAddress"] = domain.IdentityAddress;
        dict[$"{section}:IdentityCity"] = domain.IdentityCity;
        dict[$"{section}:IdentityZipcode"] = domain.IdentityZipcode;
        dict[$"{section}:IdentityCountry"] = domain.IdentityCountry;

        // CRM
        dict[$"{section}:CrmEmailTemplatePath"] = domain.CrmEmailTemplatePath;

        // Finance
        dict[$"{section}:FinanceLevel1Group"] = domain.FinanceLevel1Group;
        dict[$"{section}:FinanceLevel2Group"] = domain.FinanceLevel2Group;

        // Monitoring & Fraud Detection
        dict[$"{section}:FraudDetectionAlertRecipients"] = domain.FraudDetectionAlertRecipients;
        dict[$"{section}:FraudDetectionOperationMode"] = domain.FraudDetectionOperationMode;
        dict[$"{section}:ActiveMerchantReportRecipients"] = domain.ActiveMerchantReportRecipients;
        dict[$"{section}:BnsTransactionsWithoutEpaReportRecipients"] = domain.BnsTransactionsWithoutEpaReportRecipients;
        dict[$"{section}:IrregularityReportRecipients"] = domain.IrregularityReportRecipients;
        dict[$"{section}:DetectPhotocopyWithRefundRecipients"] = domain.DetectPhotocopyWithRefundRecipients;
        dict[$"{section}:SettlementWalletAddress"] = domain.SettlementWalletAddress;

        // External Services
        dict[$"{section}:FreshdeskApiKey"] = domain.FreshdeskApiKey;
    }
}