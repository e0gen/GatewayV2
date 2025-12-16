using System.Xml.Linq;
using Ezzygate.Application.Configuration;
using Ezzygate.Infrastructure.Cryptography;

namespace Ezzygate.Infrastructure.Configuration.Xml;

public static class XmlConfigurationReader
{
    public static ApplicationConfiguration ReadXmlConfiguration(string xmlFilePath)
    {
        if (!File.Exists(xmlFilePath))
            throw new FileNotFoundException($"XML configuration file not found: {xmlFilePath}", xmlFilePath);

        var basePath = Path.GetDirectoryName(xmlFilePath) ?? string.Empty;
        var document = XDocument.Load(xmlFilePath);
        var root = document.Root
                   ?? throw new InvalidOperationException("XML configuration file has no root element");

        var config = new ApplicationConfiguration();

        var applicationElement = root.Element("application");
        if (applicationElement != null)
            ParseApplicationParameters(applicationElement, config);

        var domainsElement = root.Element("domains");
        if (domainsElement != null)
            config.Domains = ParseDomains(domainsElement, basePath).ToList();

        var scheduledTasksElement = root.Element("scheduledTasks");
        if (scheduledTasksElement != null)
            config.ScheduledTasks = ParseScheduledTasks(scheduledTasksElement).ToList();

        return config;
    }

    private static void ParseApplicationParameters(XElement applicationElement, ApplicationConfiguration config)
    {
        var parameters = applicationElement.Elements("parameter")
            .ToDictionary(
                p => p.Attribute("key")?.Value ?? string.Empty,
                p => p.Attribute("value")?.Value ?? string.Empty,
                StringComparer.OrdinalIgnoreCase);

        config.IsProduction = GetBool(parameters, "isProduction");
        config.NumericFormat = GetString(parameters, "numericFormat", "#,#");
        config.AmountFormat = GetString(parameters, "amountFormat", "#,0.00");
        config.EnableTaskScheduler = GetBool(parameters, "enableTaskScheduler");
        config.EnableQueuedTasks = GetBool(parameters, "enableQueuedTasks");
        config.EnableEmail = GetBool(parameters, "enableEmail");
        config.SmtpHost = GetString(parameters, "smtpHost", "localhost");
        config.SmtpPort = GetInt(parameters, "smtpPort", 25);
        config.SmtpUserName = GetString(parameters, "smtpUserName");
        config.SmtpPassword = GetString(parameters, "smtpPassword");
        config.MailAddressFrom = GetString(parameters, "mailAddressFrom");
        config.MaxImmediateReportSize = GetInt(parameters, "maxImmediateReportSize", 10);
        config.MaxFiledReportSize = GetInt(parameters, "maxFiledReportSize", 10000);
        config.ErrorNetConnectionString = GetString(parameters, "errorNetConnectionString");
        config.IsErrorNetConnectionStringEncrypted = GetIsEncryptedAttribute(applicationElement, "errorNetConnectionString");
        config.LogsPhysicalBasePath = GetString(parameters, "logsPhisicalBasePath");
        config.SftpExecutablePath = GetString(parameters, "sftpExecutablePath");
        config.PgpExecutablePath = GetString(parameters, "pgpExecutablePath");
        config.CommonApplicationFiles = GetString(parameters, "commonApplicationFiles");
        config.SessionTimeout = GetInt(parameters, "SessionTimeout", 20);
        config.EnableClearCacheTimer = GetBool(parameters, "enableClearCacheTimer", true);
        config.SecurityProtocols = GetString(parameters, "securityProtocols", "Tls11,Tls12");
    }

    private static IEnumerable<DomainConfiguration> ParseDomains(XElement domainsElement, string basePath)
    {
        foreach (var domainElement in domainsElement.Elements("domain"))
        {
            var domain = new DomainConfiguration();

            var fileName = domainElement.Attribute("fileName")?.Value;
            if (!string.IsNullOrEmpty(fileName))
            {
                var externalFilePath = ResolvePath(fileName, basePath);
                if (File.Exists(externalFilePath))
                {
                    var externalDoc = XDocument.Load(externalFilePath);
                    var externalRoot = externalDoc.Root;
                    if (externalRoot != null)
                        ParseDomainParameters(externalRoot, domain);
                }
            }

            ParseDomainParameters(domainElement, domain);

            yield return domain;
        }
    }

    private static void ParseDomainParameters(XElement element, DomainConfiguration domain)
    {
        var parameterElements = element.Elements("parameter").ToList();

        var encryptionKeyNumber = 0;
        var encryptionKeyElement = parameterElements.FirstOrDefault(p =>
            p.Attribute("key")?.Value.Equals("encryptionKeyNumber", StringComparison.OrdinalIgnoreCase) == true);
        if (encryptionKeyElement != null)
        {
            var keyValue = encryptionKeyElement.Attribute("value")?.Value;
            if (!string.IsNullOrEmpty(keyValue) && int.TryParse(keyValue, out var keyNum))
            {
                encryptionKeyNumber = keyNum;
                domain.EncryptionKeyNumber = encryptionKeyNumber;
            }
        }

        var parameters = parameterElements.ToDictionary(
            p => p.Attribute("key")?.Value ?? string.Empty,
            p => GetParameterValue(p, encryptionKeyNumber),
            StringComparer.OrdinalIgnoreCase);

        // Basic Information
        if (parameters.TryGetValue("host", out var host))
            domain.Host = host;
        if (parameters.TryGetValue("legalName", out var legalName))
            domain.LegalName = legalName;
        if (parameters.TryGetValue("brandName", out var brandName))
            domain.BrandName = brandName;
        if (parameters.TryGetValue("shortCode", out var shortCode))
            domain.ShortCode = shortCode;
        if (parameters.TryGetValue("themeFolder", out var themeFolder))
            domain.ThemeFolder = themeFolder;
        if (parameters.TryGetValue("GoogleAnalyitics", out var googleAnalytics) ||
            parameters.TryGetValue("GoogleAnalytics", out googleAnalytics))
            domain.GoogleAnalytics = googleAnalytics;

        // Paths and URLs
        if (parameters.TryGetValue("DataPath", out var dataPath))
            domain.DataPath = dataPath;
        if (parameters.TryGetValue("PublicDataVirtualPath", out var publicDataVirtualPath))
            domain.PublicDataVirtualPath = publicDataVirtualPath;
        if (parameters.TryGetValue("CommonVirtualPath", out var commonVirtualPath))
            domain.CommonVirtualPath = commonVirtualPath;
        if (parameters.TryGetValue("CommonPhisicalPath", out var commonPhysicalPath) ||
            parameters.TryGetValue("CommonPhysicalPath", out commonPhysicalPath))
            domain.CommonPhysicalPath = commonPhysicalPath;

        // Security
        if (parameters.TryGetValue("netpayAdminsNTGroupName", out var netpayAdminsNTGroupName))
            domain.EzzygateAdminsNTGroupName = netpayAdminsNTGroupName;
        if (parameters.TryGetValue("ServiceUser", out var serviceUser))
            domain.ServiceUser = serviceUser;
        if (parameters.TryGetValue("ServiceUserPassword", out var serviceUserPassword))
            domain.ServiceUserPassword = serviceUserPassword;
        if (parameters.TryGetValue("WebApisMobileSecurityKey", out var webApisMobileSecurityKey))
            domain.WebApisMobileSecurityKey = webApisMobileSecurityKey;
        if (parameters.TryGetValue("WebApisMobileManualSecurityKey", out var webApisMobileManualSecurityKey))
            domain.WebApisMobileManualSecurityKey = webApisMobileManualSecurityKey;
        if (parameters.TryGetValue("WebApisMobileJwtSecretKey", out var webApisMobileJwtSecretKey))
            domain.WebApisMobileJwtSecretKey = webApisMobileJwtSecretKey;

        // Features and Flags
        if (parameters.TryGetValue("enableEpa", out var enableEpa))
            domain.EnableEpa = ParseBool(enableEpa);
        if (parameters.TryGetValue("forceSSL", out var forceSsl))
            domain.ForceSsl = ParseBool(forceSsl);
        if (parameters.TryGetValue("isHebrewVisible", out var isHebrewVisible))
            domain.IsHebrewVisible = ParseBool(isHebrewVisible);
        if (parameters.TryGetValue("SaveCui", out var saveCui))
            domain.SaveCui = ParseBool(saveCui);
        if (parameters.TryGetValue("usePublicPage", out var usePublicPage))
            domain.UsePublicPage = ParseBool(usePublicPage);
        if (parameters.TryGetValue("MultiFactor_Admin", out var multiFactorAdmin))
            domain.MultiFactorAdmin = ParseBool(multiFactorAdmin);
        if (parameters.TryGetValue("disablePostRedirectUrl", out var disablePostRedirectUrl))
            domain.DisablePostRedirectUrl = ParseBool(disablePostRedirectUrl);

        // Database Connections
        if (parameters.TryGetValue("sql1ConnectionString", out var sql1ConnectionString))
            domain.Sql1ConnectionString = sql1ConnectionString;
        if (parameters.TryGetValue("sql2ConnectionString", out var sql2ConnectionString))
            domain.Sql2ConnectionString = sql2ConnectionString;
        if (parameters.TryGetValue("reportsConnectionString", out var reportsConnectionString))
            domain.ReportsConnectionString = reportsConnectionString;
        if (parameters.TryGetValue("sqlArchiveConnectionString", out var sqlArchiveConnectionString))
            domain.SqlArchiveConnectionString = sqlArchiveConnectionString;
        if (parameters.TryGetValue("analysisServicesConnectionString", out var analysisServicesConnectionString))
            domain.AnalysisServicesConnectionString = analysisServicesConnectionString;
        if (parameters.TryGetValue("integrationServicesReportsConnectionString", out var integrationServicesReportsConnectionString))
            domain.IntegrationServicesReportsConnectionString = integrationServicesReportsConnectionString;
        if (parameters.TryGetValue("CRMConnectionString", out var crmConnectionString))
            domain.CrmConnectionString = crmConnectionString;

        // Email Settings
        if (parameters.TryGetValue("mailNameFrom", out var mailNameFrom))
            domain.MailNameFrom = mailNameFrom;
        if (parameters.TryGetValue("mailAddressFrom", out var mailAddressFrom))
            domain.MailAddressFrom = mailAddressFrom;
        if (parameters.TryGetValue("mailNameTo", out var mailNameTo))
            domain.MailNameTo = mailNameTo;
        if (parameters.TryGetValue("mailAddressTo", out var mailAddressTo))
            domain.MailAddressTo = mailAddressTo;
        if (parameters.TryGetValue("mailAddressFromChb", out var mailAddressFromChb))
            domain.MailAddressFromChb = mailAddressFromChb;
        if (parameters.TryGetValue("smtpHost", out var smtpHost))
            domain.SmtpHost = smtpHost;
        if (parameters.TryGetValue("smtpPort", out var smtpPort))
            domain.SmtpPort = smtpPort;
        if (parameters.TryGetValue("smtpUserName", out var smtpUserName))
            domain.SmtpUserName = smtpUserName;
        if (parameters.TryGetValue("smtpPassword", out var smtpPassword))
            domain.SmtpPassword = smtpPassword;

        // SMS Settings
        if (parameters.TryGetValue("SmsServiceProvider", out var smsServiceProvider))
            domain.SmsServiceProvider = smsServiceProvider;
        if (parameters.TryGetValue("SmsServiceUserName", out var smsServiceUserName))
            domain.SmsServiceUserName = smsServiceUserName;
        if (parameters.TryGetValue("SmsServicePassword", out var smsServicePassword))
            domain.SmsServicePassword = smsServicePassword;
        if (parameters.TryGetValue("SmsServiceFrom", out var smsServiceFrom))
            domain.SmsServiceFrom = smsServiceFrom;

        // Application URLs
        if (parameters.TryGetValue("merchantUrl", out var merchantUrl))
            domain.MerchantUrl = merchantUrl;
        if (parameters.TryGetValue("reportsUrl", out var reportsUrl))
            domain.ReportsUrl = reportsUrl;
        if (parameters.TryGetValue("devCentertUrl", out var devCenterUrl) ||
            parameters.TryGetValue("devCenterUrl", out devCenterUrl))
            domain.DevCenterUrl = devCenterUrl;
        if (parameters.TryGetValue("processUrl", out var processUrl))
            domain.ProcessUrl = processUrl;
        if (parameters.TryGetValue("processV2Url", out var processV2Url))
            domain.ProcessV2Url = processV2Url;
        if (parameters.TryGetValue("cartUrl", out var cartUrl))
            domain.CartUrl = cartUrl;
        if (parameters.TryGetValue("walletUrl", out var walletUrl))
            domain.WalletUrl = walletUrl;
        if (parameters.TryGetValue("WebServicesUrl", out var webServicesUrl))
            domain.WebServicesUrl = webServicesUrl;
        if (parameters.TryGetValue("WebApiUrl", out var webApiUrl))
            domain.WebApiUrl = webApiUrl;
        if (parameters.TryGetValue("partnersUrl", out var partnersUrl))
            domain.PartnersUrl = partnersUrl;
        if (parameters.TryGetValue("shopUrl", out var shopUrl))
            domain.ShopUrl = shopUrl;
        if (parameters.TryGetValue("miniVtUrl", out var miniVTUrl))
            domain.MiniVTUrl = miniVTUrl;
        if (parameters.TryGetValue("adminUrl", out var adminUrl))
            domain.AdminUrl = adminUrl;
        if (parameters.TryGetValue("contentUrl", out var contentUrl))
            domain.ContentUrl = contentUrl;
        if (parameters.TryGetValue("websiteUrl", out var websiteUrl))
            domain.WebsiteUrl = websiteUrl;
        if (parameters.TryGetValue("signupUrl", out var signupUrl))
            domain.SignupUrl = signupUrl;

        // Social Media
        if (parameters.TryGetValue("SocialMedia_Facebook", out var socialMediaFacebook))
            domain.SocialMediaFacebook = socialMediaFacebook;
        if (parameters.TryGetValue("SocialMedia_Youtube", out var socialMediaYoutube))
            domain.SocialMediaYoutube = socialMediaYoutube;
        if (parameters.TryGetValue("SocialMedia_LinkedIn", out var socialMediaLinkedIn))
            domain.SocialMediaLinkedIn = socialMediaLinkedIn;
        if (parameters.TryGetValue("SocialMedia_Twitter", out var socialMediaTwitter))
            domain.SocialMediaTwitter = socialMediaTwitter;

        // Support Information
        if (parameters.TryGetValue("CustomerService_Email", out var customerServiceEmail))
            domain.CustomerServiceEmail = customerServiceEmail;
        if (parameters.TryGetValue("CustomerService_Skype", out var customerServiceSkype))
            domain.CustomerServiceSkype = customerServiceSkype;
        if (parameters.TryGetValue("CustomerService_Fax", out var customerServiceFax))
            domain.CustomerServiceFax = customerServiceFax;
        if (parameters.TryGetValue("CustomerService_Phone", out var customerServicePhone))
            domain.CustomerServicePhone = customerServicePhone;
        if (parameters.TryGetValue("CustomerService_USPhone", out var customerServiceUSPhone))
            domain.CustomerServiceUSPhone = customerServiceUSPhone;
        if (parameters.TryGetValue("TechnicalSupport_Email", out var technicalSupportEmail))
            domain.TechnicalSupportEmail = technicalSupportEmail;
        if (parameters.TryGetValue("TechnicalSupport_Phone", out var technicalSupportPhone))
            domain.TechnicalSupportPhone = technicalSupportPhone;
        if (parameters.TryGetValue("TechnicalSupport_Fax", out var technicalSupportFax))
            domain.TechnicalSupportFax = technicalSupportFax;
        if (parameters.TryGetValue("TechnicalSupport_Skype", out var technicalSupportSkype))
            domain.TechnicalSupportSkype = technicalSupportSkype;

        // Company Information
        if (parameters.TryGetValue("Identity_Address", out var identityAddress))
            domain.IdentityAddress = identityAddress;
        if (parameters.TryGetValue("Identity_City", out var identityCity))
            domain.IdentityCity = identityCity;
        if (parameters.TryGetValue("Identity_Zipcode", out var identityZipcode))
            domain.IdentityZipcode = identityZipcode;
        if (parameters.TryGetValue("Identity_Country", out var identityCountry))
            domain.IdentityCountry = identityCountry;

        // CRM
        if (parameters.TryGetValue("CRMEmailTemplatePath", out var crmEmailTemplatePath))
            domain.CrmEmailTemplatePath = crmEmailTemplatePath;

        // Finance
        if (parameters.TryGetValue("FinanceLevel1Group", out var financeLevel1Group))
            domain.FinanceLevel1Group = financeLevel1Group;
        if (parameters.TryGetValue("FinanceLevel2Group", out var financeLevel2Group))
            domain.FinanceLevel2Group = financeLevel2Group;

        // Monitoring & Fraud Detection
        if (parameters.TryGetValue("fraudDetectionAlertRecipients", out var fraudDetectionAlertRecipients))
            domain.FraudDetectionAlertRecipients = fraudDetectionAlertRecipients;
        if (parameters.TryGetValue("FraudDetectionOperationMode", out var fraudDetectionOperationMode))
            domain.FraudDetectionOperationMode = fraudDetectionOperationMode;
        if (parameters.TryGetValue("ActiveMerchantReportRecipients", out var activeMerchantReportRecipients))
            domain.ActiveMerchantReportRecipients = activeMerchantReportRecipients;
        if (parameters.TryGetValue("BnsTransactionsWithoutEpaReportRecipients", out var bnsTransactionsWithoutEpaReportRecipients))
            domain.BnsTransactionsWithoutEpaReportRecipients = bnsTransactionsWithoutEpaReportRecipients;
        if (parameters.TryGetValue("IrregularityReportRecipients", out var irregularityReportRecipients))
            domain.IrregularityReportRecipients = irregularityReportRecipients;
        if (parameters.TryGetValue("DetectPhotocopyWithRefundRecipients", out var detectPhotocopyWithRefundRecipients))
            domain.DetectPhotocopyWithRefundRecipients = detectPhotocopyWithRefundRecipients;
        if (parameters.TryGetValue("SettlementWalletAddress", out var settlementWalletAddress))
            domain.SettlementWalletAddress = settlementWalletAddress;

        // External Services
        if (parameters.TryGetValue("FreshdeskApiKey", out var freshdeskApiKey))
            domain.FreshdeskApiKey = freshdeskApiKey;
    }

    private static IEnumerable<ScheduledTask> ParseScheduledTasks(XElement scheduledTasksElement)
    {
        foreach (var taskElement in scheduledTasksElement.Elements("task"))
        {
            var task = new ScheduledTask
            {
                GroupId = taskElement.Attribute("groupID")?.Value ?? string.Empty,
                Description = taskElement.Attribute("description")?.Value ?? string.Empty,
                Tag = taskElement.Attribute("tag")?.Value ?? string.Empty,
                AssemblyName = taskElement.Attribute("assemblyName")?.Value ?? string.Empty,
                TypeName = taskElement.Attribute("typeName")?.Value ?? string.Empty,
                MethodName = taskElement.Attribute("methodName")?.Value ?? string.Empty,
                Args = taskElement.Attribute("args")?.Value ?? string.Empty
            };

            var scheduleInfoElement = taskElement.Element("scheduleInfo");
            if (scheduleInfoElement != null)
            {
                task.Schedule = new ScheduleInfo
                {
                    Interval = scheduleInfoElement.Attribute("interval")?.Value ?? string.Empty,
                    Every = int.TryParse(scheduleInfoElement.Attribute("every")?.Value, out var every) ? every : 1,
                    Hour = int.TryParse(scheduleInfoElement.Attribute("hour")?.Value, out var hour) ? hour : null,
                    Minute = int.TryParse(scheduleInfoElement.Attribute("minute")?.Value, out var minute) ? minute : null,
                    DayOfWeek = scheduleInfoElement.Attribute("dayOfWeek")?.Value,
                    DayOfMonth = int.TryParse(scheduleInfoElement.Attribute("dayOfMonth")?.Value, out var dayOfMonth) ? dayOfMonth : null
                };
            }

            yield return task;
        }
    }

    private static string GetParameterValue(XElement paramElement, int encryptionKeyNumber)
    {
        var value = paramElement.Attribute("value")?.Value ?? string.Empty;
        var isEncryptedValue = paramElement.Attribute("isEncrypted")?.Value;

        var isEncrypted = !string.IsNullOrEmpty(isEncryptedValue) &&
                          (isEncryptedValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                           isEncryptedValue.Equals("1", StringComparison.OrdinalIgnoreCase)) &&
                          encryptionKeyNumber > 0;

        if (!isEncrypted || !CryptographyContext.IsInitialized)
            return value;

        try
        {
            value = EncryptionUtils.DecryptHex(encryptionKeyNumber, value);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to decrypt parameter '{paramElement.Attribute("key")?.Value}' with key {encryptionKeyNumber}. ");
        }

        return value;
    }

    private static string ResolvePath(string path, string basePath)
    {
        if (string.IsNullOrEmpty(path))
            return path;

        if (path.StartsWith("~/"))
        {
            path = path[2..];
            return Path.GetFullPath(Path.Combine(basePath, path));
        }

        if (path.StartsWith(@"~\") || path.StartsWith("~/../"))
        {
            path = path.TrimStart('~', '\\', '/');
            return Path.GetFullPath(Path.Combine(basePath, path));
        }

        return Path.IsPathRooted(path) ? path : Path.GetFullPath(Path.Combine(basePath, path));
    }

    private static string GetString(Dictionary<string, string> parameters, string key, string defaultValue = "")
    {
        return parameters.GetValueOrDefault(key, defaultValue);
    }

    private static int GetInt(Dictionary<string, string> parameters, string key, int defaultValue = 0)
    {
        if (parameters.TryGetValue(key, out var value) && int.TryParse(value, out var result))
            return result;
        return defaultValue;
    }

    private static bool GetBool(Dictionary<string, string> parameters, string key, bool defaultValue = false)
    {
        return parameters.TryGetValue(key, out var value) ? ParseBool(value, defaultValue) : defaultValue;
    }

    private static bool GetIsEncryptedAttribute(XElement parentElement, string parameterKey)
    {
        var element = parentElement.Elements("parameter")
            .FirstOrDefault(p => p.Attribute("key")?.Value == parameterKey);
        return element?.Attribute("isEncrypted")?.Value.ToLowerInvariant() == "true";
    }

    private static bool ParseBool(string? value, bool defaultValue = false)
    {
        if (string.IsNullOrEmpty(value))
            return defaultValue;

        return value.ToLowerInvariant() switch
        {
            "true" or "1" or "yes" => true,
            "false" or "0" or "no" => false,
            _ => defaultValue
        };
    }
}