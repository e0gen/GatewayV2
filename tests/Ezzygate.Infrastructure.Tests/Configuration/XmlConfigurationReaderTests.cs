using Ezzygate.Infrastructure.Configuration;
using Ezzygate.Infrastructure.Configuration.Xml;

namespace Ezzygate.Infrastructure.Tests.Configuration;

[TestFixture]
public class XmlConfigurationReaderTests
{
    private string _testConfigPath = null!;

    [SetUp]
    public void SetUp()
    {
        _testConfigPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "Ezzygate.Infrastructure.config.xml");
    }

    [Test]
    public void ReadXmlConfiguration_FileNotFound_ThrowsFileNotFoundException()
    {
        var nonExistentPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "NonExistent.xml");

        Assert.Throws<FileNotFoundException>(() => XmlConfigurationReader.ReadXmlConfiguration(nonExistentPath));
    }

    [Test]
    public void ReadXmlConfiguration_ValidFile_ReturnsApplicationConfiguration()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);

        Assert.That(config, Is.Not.Null);
        Assert.That(config, Is.TypeOf<ApplicationConfiguration>());
    }

    [Test]
    public void ReadXmlConfiguration_ApplicationSection_ParsesAllApplicationProperties()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);

        Assert.Multiple(() =>
        {
            Assert.That(config.IsProduction, Is.False);
            Assert.That(config.NumericFormat, Is.EqualTo("#,#"));
            Assert.That(config.AmountFormat, Is.EqualTo("#,0.00"));
            Assert.That(config.EnableTaskScheduler, Is.False);
            Assert.That(config.EnableEmail, Is.True);
            Assert.That(config.SmtpHost, Is.EqualTo("localhost"));
            Assert.That(config.SmtpPort, Is.EqualTo(25));
            Assert.That(config.SmtpUserName, Is.EqualTo("testuser"));
            Assert.That(config.MailAddressFrom, Is.EqualTo("test@netpay-intl.com"));
            Assert.That(config.MaxImmediateReportSize, Is.EqualTo(10));
            Assert.That(config.MaxFiledReportSize, Is.EqualTo(10000));
            Assert.That(config.ErrorNetConnectionString, Is.EqualTo("Data Source=TestServer;Initial Catalog=TestDB"));
            Assert.That(config.LogsPhysicalBasePath, Is.EqualTo(@"C:\Logs\"));
            Assert.That(config.SftpExecutablePath, Is.EqualTo(@"C:\WinSCP\WinSCP.com"));
            Assert.That(config.CommonApplicationFiles, Is.EqualTo(@"C:\CommonFiles\"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainsSection_ParsesCorrectCount()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);

        Assert.That(config.Domains, Has.Count.EqualTo(2));
    }

    [Test]
    public void ReadXmlConfiguration_DomainsSection_ParsesFirstDomainProperties()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("TestDomain"));
            Assert.That(domain.LegalName, Is.EqualTo("Test Domain Legal"));
            Assert.That(domain.ThemeFolder, Is.EqualTo("Tmp_Test"));
            Assert.That(domain.MultiFactorAdmin, Is.True);
            Assert.That(domain.IsHebrewVisible, Is.True);
            Assert.That(domain.FreshdeskApiKey, Is.EqualTo("test-api-key-123"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainsSection_ParsesSecondDomainProperties()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[1];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("default"));
            Assert.That(domain.DisablePostRedirectUrl, Is.True);
        });
    }

    [Test]
    public void ReadXmlConfiguration_ScheduledTasksSection_ParsesCorrectCount()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);

        Assert.That(config.ScheduledTasks, Has.Count.EqualTo(2));
    }

    [Test]
    public void ReadXmlConfiguration_ScheduledTasksSection_ParsesFirstTaskProperties()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var task = config.ScheduledTasks[0];

        Assert.Multiple(() =>
        {
            Assert.That(task.GroupId, Is.EqualTo("test"));
            Assert.That(task.Description, Is.EqualTo("test task"));
            Assert.That(task.Schedule.Interval, Is.EqualTo("Hour"));
            Assert.That(task.Schedule.Every, Is.EqualTo(2));
        });
    }

    [Test]
    public void ReadXmlConfiguration_ScheduledTasksSection_ParsesSecondTaskProperties()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var task = config.ScheduledTasks[1];

        Assert.Multiple(() =>
        {
            Assert.That(task.Schedule.Hour, Is.EqualTo(10));
            Assert.That(task.Schedule.Minute, Is.EqualTo(30));
        });
    }
}

