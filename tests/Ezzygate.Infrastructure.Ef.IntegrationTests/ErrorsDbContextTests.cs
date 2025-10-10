using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ezzygate.Infrastructure.Ef.Context;
using Ezzygate.Infrastructure.Ef.Entities;
using Ezzygate.Infrastructure.Ef.Models;
using NUnit.Framework;

namespace Ezzygate.Infrastructure.Ef.IntegrationTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Children)]
    public class ErrorsDbContextTests
    {
        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration.GetConnectionString("ErrorConnection");
        }

        private static ErrorsDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ErrorsDbContext>()
                .UseSqlServer(GetConnectionString())
                .Options;

            return new ErrorsDbContext(options);
        }

        [Test]
        public async Task BllLogs_RoundTripTest()
        {
            await using var context = CreateContext();
            var testId = Guid.NewGuid();
            var testLog = new BllLog
            {
                InsertDate = DateTime.UtcNow,
                Severity = LogSeverity.Error,
                Tag = "Test_" + testId,
                Source = "ErrorsDbContextTests",
                Message = "Test log entry " + testId
            };

            // Act - Add
            context.BllLogs.Add(testLog);
            await context.SaveChangesAsync();

            // Act - Retrieve
            await using var verifyContext = CreateContext();
            var retrievedLog = await verifyContext.BllLogs
                .FirstOrDefaultAsync(x => x.Id == testLog.Id);

            Assert.That(retrievedLog, Is.Not.Null, "Failed to retrieve the inserted log");
            Assert.Multiple(() =>
            {
                Assert.That(retrievedLog!.Tag, Is.EqualTo(testLog.Tag), "Tag mismatch");
                Assert.That(retrievedLog.Message, Is.EqualTo(testLog.Message), "Message mismatch");
            });
        }

        [Test]
        public async Task ErrorNets_RoundTripTest()
        {
            await using var context = CreateContext();
            var testId = Guid.NewGuid();
            var now = DateTime.UtcNow;

            var testError = new ErrorNet
            {
                ErrorTime = now,
                ProjectName = "IntegrationTestProject",
                RemoteIP = "192.168.1.1",
                LocalIP = "127.0.0.1",
                RemoteUser = "testuser",
                ServerName = "TEST-SERVER",
                ServerPort = "8080",
                ScriptName = "/api/error/test",
                RequestQueryString = "id=" + testId,
                VirtualPath = "/test/path",
                PhysicalPath = "C:\\sites\\test\\path",
                ExceptionSource = "TestSource",
                ExceptionMessage = "Test exception message " + testId,
                ExceptionTargetSite = "TestTargetSite",
                ExceptionStackTrace = "Test stack trace",
                ExceptionHelpLink = "https://example.com/help",
                ExceptionLineNumber = 42,
                InnerExceptionSource = "InnerTestSource",
                InnerExceptionMessage = "Inner test message " + testId,
                InnerExceptionTargetSite = "InnerTargetSite",
                InnerExceptionStackTrace = "Inner stack trace",
                InnerExceptionHelpLink = "https://example.com/inner-help",
                InnerExceptionLineNumber = 24,
                IsFailedSQL = false,
                IsArchive = false,
                RequestForm = "form=data",
                IsHighlighted = false,
                AppVersion = 1.0m,
                Domain = "test.domain.com",
                HttpCode = "500"
            };

            // Act - Add
            context.ErrorNets.Add(testError);
            await context.SaveChangesAsync();

            // Act - Retrieve
            await using var verifyContext = CreateContext();
            var actual = await verifyContext.ErrorNets
                .FirstOrDefaultAsync(x => x.Id == testError.Id);

            Assert.That(actual, Is.Not.Null, "Failed to retrieve the inserted error");
            Assert.Multiple(() =>
            {
                Assert.That(actual!.ErrorTime, Is.EqualTo(now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(actual.ProjectName, Is.EqualTo(testError.ProjectName));
                Assert.That(actual.RemoteIP, Is.EqualTo(testError.RemoteIP));
                Assert.That(actual.LocalIP, Is.EqualTo(testError.LocalIP));
                Assert.That(actual.RemoteUser, Is.EqualTo(testError.RemoteUser));
                Assert.That(actual.ServerName, Is.EqualTo(testError.ServerName));
                Assert.That(actual.ServerPort, Is.EqualTo(testError.ServerPort));
                Assert.That(actual.ScriptName, Is.EqualTo(testError.ScriptName));
                Assert.That(actual.RequestQueryString, Is.EqualTo(testError.RequestQueryString));
                Assert.That(actual.VirtualPath, Is.EqualTo(testError.VirtualPath));
                Assert.That(actual.PhysicalPath, Is.EqualTo(testError.PhysicalPath));
                Assert.That(actual.ExceptionSource, Is.EqualTo(testError.ExceptionSource));
                Assert.That(actual.ExceptionMessage, Is.EqualTo(testError.ExceptionMessage));
                Assert.That(actual.ExceptionTargetSite, Is.EqualTo(testError.ExceptionTargetSite));
                Assert.That(actual.ExceptionStackTrace, Is.EqualTo(testError.ExceptionStackTrace));
                Assert.That(actual.ExceptionHelpLink, Is.EqualTo(testError.ExceptionHelpLink));
                Assert.That(actual.ExceptionLineNumber, Is.EqualTo(testError.ExceptionLineNumber));
                Assert.That(actual.InnerExceptionSource, Is.EqualTo(testError.InnerExceptionSource));
                Assert.That(actual.InnerExceptionMessage, Is.EqualTo(testError.InnerExceptionMessage));
                Assert.That(actual.InnerExceptionTargetSite, Is.EqualTo(testError.InnerExceptionTargetSite));
                Assert.That(actual.InnerExceptionStackTrace, Is.EqualTo(testError.InnerExceptionStackTrace));
                Assert.That(actual.InnerExceptionHelpLink, Is.EqualTo(testError.InnerExceptionHelpLink));
                Assert.That(actual.InnerExceptionLineNumber, Is.EqualTo(testError.InnerExceptionLineNumber));
                Assert.That(actual.IsFailedSQL, Is.EqualTo(testError.IsFailedSQL));
                Assert.That(actual.IsArchive, Is.EqualTo(testError.IsArchive));
                Assert.That(actual.RequestForm, Is.EqualTo(testError.RequestForm));
                Assert.That(actual.IsHighlighted, Is.EqualTo(testError.IsHighlighted));
                Assert.That(actual.AppVersion, Is.EqualTo(testError.AppVersion));
                Assert.That(actual.Domain, Is.EqualTo(testError.Domain));
                Assert.That(actual.HttpCode, Is.EqualTo(testError.HttpCode));
            });
        }
    }
}