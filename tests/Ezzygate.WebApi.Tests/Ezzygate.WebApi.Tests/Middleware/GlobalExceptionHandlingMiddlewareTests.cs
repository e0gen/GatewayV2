using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Ezzygate.Application.Integrations;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Middleware;

namespace Ezzygate.WebApi.Tests.Middleware;

[TestFixture]
public class GlobalExceptionHandlingMiddlewareTests
{
    private GlobalExceptionHandlingMiddleware _middleware;
    private Mock<RequestDelegate> _nextMock;
    private Mock<IWebHostEnvironment> _environmentMock;
    private HttpContext _httpContext;
    private JsonSerializerOptions _jsonSerializerOptions;

    [SetUp]
    public void SetUp()
    {
        _nextMock = new Mock<RequestDelegate>();
        _environmentMock = new Mock<IWebHostEnvironment>();
        _httpContext = new DefaultHttpContext();
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        _middleware = new GlobalExceptionHandlingMiddleware(_nextMock.Object, _environmentMock.Object);
    }

    [Test]
    public async Task InvokeAsync_WhenNoException_CallsNextDelegate()
    {
        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        _nextMock.Verify(x => x(_httpContext), Times.Once);
    }

    [Test]
    public async Task InvokeAsync_WhenExceptionThrown_InDevelopment_ReturnsExceptionDetails()
    {
        var exception = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(exception);
        _environmentMock.Setup(x => x.EnvironmentName).Returns("Development");
        _httpContext.Response.Body = new MemoryStream();

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(response, _jsonSerializerOptions);
        Assert.That(jsonElement.GetProperty("result").GetString(), Is.EqualTo(nameof(ResultEnum.GeneralError)));
        Assert.That(jsonElement.GetProperty("data").GetString(), Is.EqualTo(exception.ToString()));
    }

    [Test]
    public async Task InvokeAsync_WhenExceptionThrown_InProduction_ReturnsGenericError()
    {
        var exception = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(exception);
        _environmentMock.Setup(x => x.EnvironmentName).Returns("Production");
        _httpContext.Response.Body = new MemoryStream();

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(response, _jsonSerializerOptions);
        Assert.That(jsonElement.GetProperty("result").GetString(), Is.EqualTo(nameof(ResultEnum.GeneralError)));
        Assert.That(jsonElement.GetProperty("data").GetString(), Is.EqualTo("General error, see logs."));
    }

    [Test]
    public async Task InvokeAsync_WhenExceptionThrown_ForIntegrationController_ReturnsIntegrationResult()
    {
        var exception = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(exception);
        _environmentMock.Setup(x => x.EnvironmentName).Returns("Development");
        _httpContext.Response.Body = new MemoryStream();
        _httpContext.Request.RouteValues["controller"] = "Integration";

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        var result = JsonSerializer.Deserialize<IntegrationResult>(response, _jsonSerializerOptions)!;
        Assert.That(result.Code, Is.EqualTo("520"));
        Assert.That(result.Message, Is.EqualTo(exception.ToString()));
    }

    [Test]
    public async Task InvokeAsync_WhenExceptionThrown_ForNonIntegrationController_ReturnsStandardResponse()
    {
        var exception = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(exception);
        _environmentMock.Setup(x => x.EnvironmentName).Returns("Development");
        _httpContext.Response.Body = new MemoryStream();
        _httpContext.Request.RouteValues["controller"] = "Payment";

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(response, _jsonSerializerOptions);
        Assert.That(jsonElement.GetProperty("result").GetString(), Is.EqualTo(nameof(ResultEnum.GeneralError)));
        Assert.That(jsonElement.GetProperty("data").GetString(), Is.EqualTo(exception.ToString()));
    }

    [Test]
    public async Task InvokeAsync_WhenExceptionThrown_WithNullController_ReturnsStandardResponse()
    {
        var exception = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(exception);
        _environmentMock.Setup(x => x.EnvironmentName).Returns("Development");
        _httpContext.Response.Body = new MemoryStream();
        _httpContext.Request.RouteValues["controller"] = null;

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        Assert.That(_httpContext.Response.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));

        _httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(_httpContext.Response.Body);
        var response = await reader.ReadToEndAsync();

        var jsonElement = JsonSerializer.Deserialize<JsonElement>(response, _jsonSerializerOptions);
        Assert.That(jsonElement.GetProperty("result").GetString(), Is.EqualTo(nameof(ResultEnum.GeneralError)));
        Assert.That(jsonElement.GetProperty("data").GetString(), Is.EqualTo(exception.ToString()));
    }
}