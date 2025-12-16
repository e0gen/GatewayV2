using System.Runtime.Versioning;
using Ezzygate.Application.Configuration;
using Ezzygate.Infrastructure.Configuration.Xml;
using Ezzygate.Infrastructure.Cryptography;
using Ezzygate.Infrastructure.Win32.Cryptography.Providers;

namespace Ezzygate.Infrastructure.Tests.Configuration.Xml;

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
            Assert.That(config.SmtpUserName, Is.EqualTo("test-user"));
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

        Assert.That(config.Domains, Has.Count.EqualTo(3));
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

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_LoadsValuesFromEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.ShortCode, Is.EqualTo("TEST"));
            Assert.That(domain.BrandName, Is.EqualTo("TestBrand"));
            Assert.That(domain.EncryptionKeyNumber, Is.EqualTo(999));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_LoadsConnectionStringsFromEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Sql1ConnectionString, Is.EqualTo("Data Source=EnvTestServer;Initial Catalog=EnvTestDB"));
            Assert.That(domain.Sql2ConnectionString, Is.EqualTo("C70F0CF5C4E6985AB8696662BEC22F5C8B64405815667061BADEC72E628531F851776A55B8EA686280CBA60EE9E150C6AF0A25E88A4D3114DDD8986AD4ACA825"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_LoadsUrlsFromEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.AdminUrl, Is.EqualTo("https://admin.test-env.local/"));
            Assert.That(domain.MerchantUrl, Is.EqualTo("https://merchant.test-env.local/"));
            Assert.That(domain.ReportsUrl, Is.EqualTo("https://reports.test-env.local/"));
            Assert.That(domain.WebApiUrl, Is.EqualTo("https://api.test-env.local/"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_LoadsSmtpFromEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.SmtpHost, Is.EqualTo("smtp.test-environment.local"));
            Assert.That(domain.SmtpPort, Is.EqualTo("587"));
            Assert.That(domain.SmtpUserName, Is.EqualTo("env-user@test.local"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_InlineValuesOverrideEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("TestDomain"));
            Assert.That(domain.LegalName, Is.EqualTo("Test Domain Legal"));
            Assert.That(domain.ThemeFolder, Is.EqualTo("Tmp_Test"));
            Assert.That(domain.FreshdeskApiKey, Is.EqualTo("test-api-key-123"));
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_InlineBooleanOverridesEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.That(domain.IsHebrewVisible, Is.True);
        Assert.That(domain.EnableEpa, Is.False);
        Assert.That(domain.ForceSsl, Is.True);
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithEnvFile_InlinePathOverridesEnvironmentFile()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[0];

        Assert.That(domain.DataPath, Is.EqualTo(@"\FilesTest\"));
        Assert.That(domain.PublicDataVirtualPath, Is.EqualTo("Data/"));
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithoutEnvFile_OnlyUsesInlineValues()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[1];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("default"));
            Assert.That(domain.LegalName, Is.EqualTo("Default Domain"));
            Assert.That(domain.ShortCode, Is.Empty.Or.Null);
            Assert.That(domain.BrandName, Is.Empty.Or.Null);
            Assert.That(domain.Sql1ConnectionString, Is.Empty.Or.Null);
        });
    }

    [Test]
    public void ReadXmlConfiguration_DomainWithMissingEnvFile_LoadsInlineValues()
    {
        var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
        var domain = config.Domains[2];

        Assert.Multiple(() =>
        {
            Assert.That(domain.Host, Is.EqualTo("MissingEnvDomain"));
            Assert.That(domain.LegalName, Is.EqualTo("Missing Env Domain Legal"));
            Assert.That(domain.ThemeFolder, Is.EqualTo("Tmp_MissingEnv"));
            Assert.That(domain.ShortCode, Is.Empty.Or.Null);
        });
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public void ReadXmlConfiguration_EncryptedValueWithWindowsProvider_DecryptsCorrectly()
    {
        var testKeysPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "TestKeys");

        if (!Directory.Exists(testKeysPath))
            Directory.CreateDirectory(testKeysPath);

        var factory = new WindowsCryptographyProviderFactory();
        var cryptoConfig = new CryptographyConfiguration
        {
            DefaultKeyStore = testKeysPath
        };

        CryptographyContext.Reset();
        CryptographyContext.Initialize(factory, cryptoConfig);

        try
        {
            var config = XmlConfigurationReader.ReadXmlConfiguration(_testConfigPath);
            var domain = config.Domains.Single(x => x.Host == "TestDomain");

            const string expectedDecryptedValue = "Data Source=EnvTestServer;Initial Catalog=EnvTestDB2";

            Assert.Multiple(() =>
            {
                Assert.That(domain.EncryptionKeyNumber, Is.EqualTo(999));
                Assert.That(domain.Sql2ConnectionString, Is.EqualTo(expectedDecryptedValue));
            });
        }
        finally
        {
            SymEncryption.ClearKeyCache();
            CryptographyContext.Reset();
        }
    }
}