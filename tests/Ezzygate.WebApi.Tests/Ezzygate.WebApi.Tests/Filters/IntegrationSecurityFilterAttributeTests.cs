using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Filters;

namespace Ezzygate.WebApi.Tests.Filters;

[TestFixture]
public class IntegrationSecurityFilterAttributeTests
{
    private Mock<IServiceProvider> _serviceProviderMock;
    private ActionExecutingContext _actionExecutingContext;
    private ActionExecutedContext _actionExecutedContext;
    private IntegrationSecurityFilterAttribute _filter;
    private DefaultHttpContext _httpContext;

    [SetUp]
    public void SetUp()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProviderMock.Object,
            Request = { Body = new MemoryStream() },
            Connection = { RemoteIpAddress = System.Net.IPAddress.Loopback }
        };

        var actionDescriptor = new ControllerActionDescriptor();
        var routeData = new RouteData();

        var actionContext = new ActionContext(_httpContext, routeData, actionDescriptor);
        _actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object?>(), new Mock<ControllerBase>().Object);
        _actionExecutedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new Mock<ControllerBase>().Object);

        _filter = new IntegrationSecurityFilterAttribute();
    }

    private (ActionExecutionDelegate next, Func<bool> wasCalled) CreateNextDelegateWithTracker()
    {
        var nextCalled = false;
        var next = new ActionExecutionDelegate(() =>
        {
            nextCalled = true;
            return Task.FromResult(new ActionExecutedContext(_actionExecutingContext, _actionExecutingContext.Filters, _actionExecutingContext.Controller));
        });
        return (next, () => nextCalled);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithValidSignature_AllowsExecution()
    {
        const string content = "test content";
        var expectedSignature = HashUtils.ComputeSha256Hash(content + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        
        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithMissingSignature_ReturnsSignatureRequiredError()
    {
        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<OkObjectResult>());
        
        var result = _actionExecutingContext.Result as OkObjectResult;
        var integrationResult = result!.Value as IntegrationResult;
        Assert.That(integrationResult!.Code, Is.EqualTo("520"));
        Assert.That(integrationResult.Message, Is.EqualTo(nameof(ResultEnum.SignatureRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithInvalidSignature_ReturnsSignatureMismatchError()
    {
        const string content = "test content";
        const string invalidSignature = "invalid_signature";
        
        _httpContext.Request.Headers["signature"] = invalidSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<OkObjectResult>());
        
        var result = _actionExecutingContext.Result as OkObjectResult;
        var integrationResult = result!.Value as IntegrationResult;
        Assert.That(integrationResult!.Code, Is.EqualTo("520"));
        Assert.That(integrationResult.Message, Is.EqualTo(nameof(ResultEnum.SignatureMismatch)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithNonLocalConnectionAndNonHttps_ReturnsSslRequiredError()
    {
        const string content = "test content";
        var expectedSignature = HashUtils.ComputeSha256Hash(content + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        
        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "http";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<OkObjectResult>());
        
        var result = _actionExecutingContext.Result as OkObjectResult;
        var integrationResult = result!.Value as IntegrationResult;
        Assert.That(integrationResult!.Code, Is.EqualTo("520"));
        Assert.That(integrationResult.Message, Is.EqualTo(nameof(ResultEnum.SslRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithLocalConnectionAndHttp_AllowsExecution()
    {
        const string content = "test content";
        var expectedSignature = HashUtils.ComputeSha256Hash(content + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        
        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "http";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Loopback;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithNonLocalConnectionAndHttps_AllowsExecution()
    {
        const string content = "test content";
        var expectedSignature = HashUtils.ComputeSha256Hash(content + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        
        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "https";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public void OnActionExecuted_WithObjectResult_AddsSignatureHeader()
    {
        var responseData = new IntegrationResult { Code = "200", Message = "Success" };
        var objectResult = new OkObjectResult(responseData);
        _actionExecutedContext.Result = objectResult;

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.True);
        
        var expectedContent = System.Text.Json.JsonSerializer.Serialize(responseData);
        var expectedSignature = HashUtils.ComputeSha256Hash(expectedContent + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        Assert.That(_httpContext.Response.Headers["signature"], Is.EqualTo(expectedSignature));
    }

    [Test]
    public void OnActionExecuted_WithNullObjectResult_DoesNotAddSignatureHeader()
    {
        var objectResult = new OkObjectResult(null);
        _actionExecutedContext.Result = objectResult;

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.False);
    }

    [Test]
    public void OnActionExecuted_WithNonObjectResult_DoesNotAddSignatureHeader()
    {
        _actionExecutedContext.Result = new EmptyResult();

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.False);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithEmptyBody_CalculatesSignatureCorrectly()
    {
        const string content = "";
        var expectedSignature = HashUtils.ComputeSha256Hash(content + "c3722f2e-d476-40a5-abea-b4c5b66a8891");
        
        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }
}
