namespace Ezzygate.Infrastructure.Configuration;

public class ApplicationConfiguration
{
    public const string SectionName = "Application";

    // Application settings
    public bool IsProduction { get; set; }
    public string NumericFormat { get; set; } = "#,#";
    public string AmountFormat { get; set; } = "#,0.00";
    public bool EnableTaskScheduler { get; set; }
    public bool EnableQueuedTasks { get; set; }

    // Email settings
    public bool EnableEmail { get; set; }
    public string SmtpHost { get; set; } = "localhost";
    public int SmtpPort { get; set; } = 25;
    public string SmtpUserName { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
    public string MailAddressFrom { get; set; } = string.Empty;

    // Report settings
    public int MaxImmediateReportSize { get; set; } = 10;
    public int MaxFiledReportSize { get; set; } = 10000;

    // Connection strings and paths
    public string ErrorNetConnectionString { get; set; } = string.Empty;
    public bool IsErrorNetConnectionStringEncrypted { get; set; }
    public string LogsPhysicalBasePath { get; set; } = string.Empty;
    public string SftpExecutablePath { get; set; } = string.Empty;
    public string PgpExecutablePath { get; set; } = string.Empty;
    public string CommonApplicationFiles { get; set; } = string.Empty;

    public List<DomainConfiguration> Domains { get; set; } = new();

    [Obsolete("Legacy")]
    public List<ScheduledTask> ScheduledTasks { get; set; } = new();
}