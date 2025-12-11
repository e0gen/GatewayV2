using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Configuration.Xml;

namespace Ezzygate.Infrastructure.Tests.Configuration.Xml;

[TestFixture]
public class XmlConfigurationExtensionsTests
{
    private string _testConfigPath = null!;

    [SetUp]
    public void SetUp()
    {
        _testConfigPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData");
    }

    private IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .AddXmlInfrastructureConfigurationSource(_testConfigPath)
            .Build();
    }

    private ApplicationConfiguration GetApplicationConfiguration()
    {
        var configuration = BuildConfiguration();
        var services = new ServiceCollection();
        services.Configure<ApplicationConfiguration>(configuration.GetSection(ApplicationConfiguration.SectionName));
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IOptions<ApplicationConfiguration>>().Value;
    }

    [Test]
    public void AddXmlInfrastructureConfigurationSource_FileNotFound_ThrowsFileNotFoundException()
    {
        var builder = new ConfigurationBuilder();
        var nonExistentPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "NonExistent.xml");

        Assert.Throws<FileNotFoundException>(() => builder.AddXmlInfrastructureConfigurationSource(nonExistentPath));
    }

    [Test]
    public void AddXmlInfrastructureConfigurationSource_ValidFile_BuildsConfiguration()
    {
        var configuration = BuildConfiguration();

        Assert.That(configuration, Is.Not.Null);
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsAllApplicationProperties()
    {
        var config = GetApplicationConfiguration();

        Assert.Multiple(() =>
        {
            Assert.That(config.IsProduction, Is.False);
            Assert.That(config.NumericFormat, Is.EqualTo("#,#"));
            Assert.That(config.AmountFormat, Is.EqualTo("#,0.00"));
            Assert.That(config.EnableTaskScheduler, Is.False);
            Assert.That(config.EnableQueuedTasks, Is.False);
            Assert.That(config.EnableEmail, Is.True);
            Assert.That(config.SmtpHost, Is.EqualTo("localhost"));
            Assert.That(config.SmtpPort, Is.EqualTo(25));
            Assert.That(config.SmtpUserName, Is.EqualTo("testuser"));
            Assert.That(config.SmtpPassword, Is.EqualTo("testpass"));
            Assert.That(config.MailAddressFrom, Is.EqualTo("test@netpay-intl.com"));
            Assert.That(config.MaxImmediateReportSize, Is.EqualTo(10));
            Assert.That(config.MaxFiledReportSize, Is.EqualTo(10000));
            Assert.That(config.ErrorNetConnectionString, Is.EqualTo("Data Source=TestServer;Initial Catalog=TestDB"));
            Assert.That(config.LogsPhysicalBasePath, Is.EqualTo(@"C:\Logs\"));
            Assert.That(config.SftpExecutablePath, Is.EqualTo(@"C:\WinSCP\WinSCP.com"));
            Assert.That(config.PgpExecutablePath, Is.EqualTo(@"C:\GnuPG\gpg2.exe"));
            Assert.That(config.CommonApplicationFiles, Is.EqualTo(@"C:\CommonFiles\"));
        });
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsDomainsCollection()
    {
        var config = GetApplicationConfiguration();

        Assert.That(config.Domains, Has.Count.EqualTo(3));
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsFirstDomainProperties()
    {
        var config = GetApplicationConfiguration();
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("TestDomain"));
            Assert.That(domain.LegalName, Is.EqualTo("Test Domain Legal"));
            Assert.That(domain.ThemeFolder, Is.EqualTo("Tmp_Test"));
            Assert.That(domain.DataPath, Is.EqualTo(@"\FilesTest\"));
            Assert.That(domain.PublicDataVirtualPath, Is.EqualTo("Data/"));
            Assert.That(domain.MultiFactorAdmin, Is.True);
            Assert.That(domain.DisablePostRedirectUrl, Is.False);
            Assert.That(domain.FraudDetectionAlertRecipients, Is.EqualTo("alert@test.com"));
            Assert.That(domain.FraudDetectionOperationMode, Is.EqualTo("TestMode"));
            Assert.That(domain.IsHebrewVisible, Is.True);
            Assert.That(domain.FreshdeskApiKey, Is.EqualTo("test-api-key-123"));
        });
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsSecondDomainProperties()
    {
        var config = GetApplicationConfiguration();
        var domain = config.Domains[1];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("default"));
            Assert.That(domain.LegalName, Is.EqualTo("Default Domain"));
            Assert.That(domain.DisablePostRedirectUrl, Is.True);
            Assert.That(domain.MultiFactorAdmin, Is.False);
        });
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsScheduledTasksCollection()
    {
        var config = GetApplicationConfiguration();

        Assert.That(config.ScheduledTasks, Has.Count.EqualTo(2));
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsFirstScheduledTaskProperties()
    {
        var config = GetApplicationConfiguration();
        var task = config.ScheduledTasks[0];

        Assert.Multiple(() =>
        {
            Assert.That(task.GroupId, Is.EqualTo("test"));
            Assert.That(task.Description, Is.EqualTo("test task"));
            Assert.That(task.Tag, Is.EqualTo("Test"));
            Assert.That(task.AssemblyName, Is.EqualTo("Test.Assembly"));
            Assert.That(task.TypeName, Is.EqualTo("Test.Type"));
            Assert.That(task.MethodName, Is.EqualTo("TestMethod"));
            Assert.That(task.Args, Is.EqualTo("arg1"));
            Assert.That(task.Schedule.Interval, Is.EqualTo("Hour"));
            Assert.That(task.Schedule.Every, Is.EqualTo(2));
        });
    }

    [Test]
    public void Configure_ApplicationConfiguration_BindsSecondScheduledTaskProperties()
    {
        var config = GetApplicationConfiguration();
        var task = config.ScheduledTasks[1];

        Assert.Multiple(() =>
        {
            Assert.That(task.Schedule.Hour, Is.EqualTo(10));
            Assert.That(task.Schedule.Minute, Is.EqualTo(30));
        });
    }
}