namespace Ezzygate.Infrastructure.Configuration;

public class ApplicationConfiguration
{
    public const string SectionName = "Application";

    // Application settings
    public string InstanceName { get; set; } = string.Empty;
    public bool IsProduction { get; set; }
    public bool EnableClearCacheTimer { get; set; } = true;
    public string NumericFormat { get; set; } = "#,#";
    public string AmountFormat { get; set; } = "#,0.00";
    public int SessionTimeout { get; set; } = 20;
    
    // Task scheduling
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
    public int MaxAdminFileManagerUploadSizeBytes { get; set; } = 4_000_000;
    public int MaxFiledReportSize { get; set; } = 10000;

    // Paths and directories
    public string PhysicalPath { get; set; } = string.Empty;
    public string ApplicationPath { get; set; } = string.Empty;
    public string CommonApplicationFiles { get; set; } = string.Empty;
    public string LogsPhysicalBasePath { get; set; } = string.Empty;
    
    // External tools
    public string SftpExecutablePath { get; set; } = string.Empty;
    public string PgpExecutablePath { get; set; } = string.Empty;

    // Connection strings
    public string ErrorNetConnectionString { get; set; } = string.Empty;
    public bool IsErrorNetConnectionStringEncrypted { get; set; }
    
    // Security
    public string SecurityProtocols { get; set; } = "Tls11,Tls12";
    
    // Version information
    public string Version { get; set; } = string.Empty;
    public string FullVersion { get; set; } = string.Empty;
    
    // Collections
    public List<DomainConfiguration> Domains { get; set; } = [];
    public List<ScheduledTask> ScheduledTasks { get; set; } = [];
}