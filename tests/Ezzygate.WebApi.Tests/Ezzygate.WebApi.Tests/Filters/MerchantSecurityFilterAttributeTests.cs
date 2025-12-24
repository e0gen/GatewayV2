using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Ezzygate.Domain.Models;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Filters;

namespace Ezzygate.WebApi.Tests.Filters;

[TestFixture]
public class MerchantSecurityFilterAttributeTests
{
    private Mock<IServiceProvider> _serviceProviderMock;
    private Mock<IMerchantRepository> _merchantRepositoryMock;
    private Mock<IRequestIdRepository> _requestIdRepositoryMock;
    private ActionExecutingContext _actionExecutingContext;
    private ActionExecutedContext _actionExecutedContext;
    private MerchantSecurityFilterAttribute _filter;
    private DefaultHttpContext _httpContext;
    private Merchant _testMerchant;

    [SetUp]
    public void SetUp()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _merchantRepositoryMock = new Mock<IMerchantRepository>();
        _requestIdRepositoryMock = new Mock<IRequestIdRepository>();
        _httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProviderMock.Object,
            Request = { Body = new MemoryStream() },
            Connection = { RemoteIpAddress = System.Net.IPAddress.Loopback }
        };

        _serviceProviderMock
            .Setup(x => x.GetService(typeof(IMerchantRepository)))
            .Returns(_merchantRepositoryMock.Object);

        _serviceProviderMock
            .Setup(x => x.GetService(typeof(IRequestIdRepository)))
            .Returns(_requestIdRepositoryMock.Object);

        _testMerchant = new Merchant
        {
            Id = 1,
            CustomerNumber = "TEST123",
            HashKey = "test_hash_key",
            Account = new Account { HashKey = "account_hash_key" }
        };

        var actionDescriptor = new ControllerActionDescriptor();
        var routeData = new RouteData();

        var actionContext = new ActionContext(_httpContext, routeData, actionDescriptor);
        _actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object?>(), new Mock<ControllerBase>().Object);
        _actionExecutedContext = new ActionExecutedContext(actionContext, new List<IFilterMetadata>(), new Mock<ControllerBase>().Object);

        _filter = new MerchantSecurityFilterAttribute();
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
        const string merchantNumber = "TEST123";
        var clientRequestId = Guid.NewGuid().ToString();
        var expectedSignature = HashUtils.ComputeSha256Hash(content + clientRequestId + _testMerchant.HashKey);

        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(false);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
        Assert.That(_httpContext.GetMerchant(), Is.EqualTo(_testMerchant));
        _requestIdRepositoryMock.Verify(x => x.MarkRequestIdAsUsedAsync(clientRequestId), Times.Once);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithMissingSignature_ReturnsSignatureRequiredError()
    {
        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.SignatureRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithMissingRequestId_ReturnsRequestIdRequiredError()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";

        _httpContext.Request.Headers["signature"] = "some_signature";
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.RequestIdRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithMissingMerchantNumber_ReturnsMerchantNumberRequiredError()
    {
        const string content = "test content";
        var clientRequestId = Guid.NewGuid().ToString();

        _httpContext.Request.Headers["signature"] = "some_signature";
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.MerchantNumberRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithInvalidMerchant_ReturnsMerchantNotFoundError()
    {
        const string content = "test content";
        const string merchantNumber = "NONEXISTENT";
        var clientRequestId = Guid.NewGuid().ToString();

        _httpContext.Request.Headers["signature"] = "some_signature";
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync((Merchant?)null);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.MerchantNotFound)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithInvalidRequestId_ReturnsInvalidRequestIdError()
    {
        const string content = "test content";
        const string invalidRequestId = "invalid_guid";
        const string merchantNumber = "TEST123";

        _httpContext.Request.Headers["signature"] = "some_signature";
        _httpContext.Request.Headers["request-id"] = invalidRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequestId)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithDuplicateRequestId_ReturnsDuplicateRequestIdError()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";
        var clientRequestId = Guid.NewGuid().ToString();

        _httpContext.Request.Headers["signature"] = "some_signature";
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(true);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.DuplicateRequestId)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithInvalidSignature_ReturnsSignatureMismatchError()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";
        const string invalidSignature = "invalid_signature";
        var clientRequestId = Guid.NewGuid().ToString();

        _httpContext.Request.Headers["signature"] = invalidSignature;
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(false);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.SignatureMismatch)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithNonLocalConnectionAndNonHttps_ReturnsSslRequiredError()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";
        var clientRequestId = Guid.NewGuid().ToString();
        var expectedSignature = HashUtils.ComputeSha256Hash(content + clientRequestId + _testMerchant.HashKey);

        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "http";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(false);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<BadRequestObjectResult>());

        var result = _actionExecutingContext.Result as BadRequestObjectResult;
        var response = result!.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.SslRequired)));
    }

    [Test]
    public async Task OnActionExecutionAsync_WithLocalConnectionAndHttp_AllowsExecution()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";
        var clientRequestId = Guid.NewGuid().ToString();
        var expectedSignature = HashUtils.ComputeSha256Hash(content + clientRequestId + _testMerchant.HashKey);

        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "http";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Loopback;

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(false);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithNonLocalConnectionAndHttps_AllowsExecution()
    {
        const string content = "test content";
        const string merchantNumber = "TEST123";
        var clientRequestId = Guid.NewGuid().ToString();
        var expectedSignature = HashUtils.ComputeSha256Hash(content + clientRequestId + _testMerchant.HashKey);

        _httpContext.Request.Headers["signature"] = expectedSignature;
        _httpContext.Request.Headers["request-id"] = clientRequestId;
        _httpContext.Request.Headers["merchant-number"] = merchantNumber;
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;
        _httpContext.Request.Scheme = "https";
        _httpContext.Connection.RemoteIpAddress = System.Net.IPAddress.Parse("192.168.1.1");

        _merchantRepositoryMock
            .Setup(x => x.GetByMerchantNumberAsync(merchantNumber))
            .ReturnsAsync(_testMerchant);

        _requestIdRepositoryMock
            .Setup(x => x.IsRequestIdUsedAsync(clientRequestId))
            .ReturnsAsync(false);

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await _filter.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public async Task OnActionExecutionAsync_WithRequireSignatureFalse_SkipsSignatureValidation()
    {
        var filterWithoutSignature = new MerchantSecurityFilterAttribute(false);
        const string content = "test content";

        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
        _httpContext.Request.ContentLength = content.Length;

        var (next, wasCalled) = CreateNextDelegateWithTracker();

        await filterWithoutSignature.OnActionExecutionAsync(_actionExecutingContext, next);

        Assert.That(wasCalled(), Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public void OnActionExecuted_WithObjectResult_AddsSignatureAndRequestIdHeaders()
    {
        var responseData = new Response(ResultEnum.Success, "Success message");
        var objectResult = new OkObjectResult(responseData);
        _actionExecutedContext.Result = objectResult;
        _httpContext.SetMerchant(_testMerchant);

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.True);
        Assert.That(_httpContext.Response.Headers.ContainsKey("request-id"), Is.True);

        var requestId = _httpContext.Response.Headers["request-id"];
        Assert.That(Guid.TryParse(requestId, out _), Is.True);

        var expectedContent = System.Text.Json.JsonSerializer.Serialize(responseData);
        var expectedSignature = HashUtils.ComputeSha256Hash(expectedContent + requestId + _testMerchant.Account!.HashKey);
        Assert.That(_httpContext.Response.Headers["signature"], Is.EqualTo(expectedSignature));
    }

    [Test]
    public void OnActionExecuted_WithNullMerchant_UsesDefaultHashKey()
    {
        var responseData = new Response(ResultEnum.Success, "Success message");
        var objectResult = new OkObjectResult(responseData);
        _actionExecutedContext.Result = objectResult;
        _httpContext.SetMerchant(null);

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.True);

        var requestId = _httpContext.Response.Headers["request-id"];
        var expectedContent = System.Text.Json.JsonSerializer.Serialize(responseData);
        var expectedSignature = HashUtils.ComputeSha256Hash(expectedContent + requestId + "defaultHashKey");
        Assert.That(_httpContext.Response.Headers["signature"], Is.EqualTo(expectedSignature));
    }

    [Test]
    public void OnActionExecuted_WithMerchantWithoutAccount_UsesMerchantHashKey()
    {
        var merchantWithoutAccount = new Merchant
        {
            Id = 1,
            CustomerNumber = "TEST123",
            HashKey = "merchant_hash_key",
            Account = null
        };

        var responseData = new Response(ResultEnum.Success, "Success message");
        var objectResult = new OkObjectResult(responseData);
        _actionExecutedContext.Result = objectResult;
        _httpContext.SetMerchant(merchantWithoutAccount);

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.True);

        var requestId = _httpContext.Response.Headers["request-id"];
        var expectedContent = System.Text.Json.JsonSerializer.Serialize(responseData);
        var expectedSignature = HashUtils.ComputeSha256Hash(expectedContent + requestId + "merchant_hash_key");
        Assert.That(_httpContext.Response.Headers["signature"], Is.EqualTo(expectedSignature));
    }

    [Test]
    public void OnActionExecuted_WithNullObjectResult_DoesNotAddHeaders()
    {
        var objectResult = new OkObjectResult(null);
        _actionExecutedContext.Result = objectResult;
        _httpContext.SetMerchant(_testMerchant);

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.False);
        Assert.That(_httpContext.Response.Headers.ContainsKey("request-id"), Is.False);
    }

    [Test]
    public void OnActionExecuted_WithNonObjectResult_DoesNotAddHeaders()
    {
        _actionExecutedContext.Result = new EmptyResult();
        _httpContext.SetMerchant(_testMerchant);

        _filter.OnActionExecuted(_actionExecutedContext);

        Assert.That(_httpContext.Response.Headers.ContainsKey("signature"), Is.False);
        Assert.That(_httpContext.Response.Headers.ContainsKey("request-id"), Is.False);
    }
}