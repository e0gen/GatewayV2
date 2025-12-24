using Microsoft.AspNetCore.Http;
using Ezzygate.WebApi.Middleware;

namespace Ezzygate.WebApi.Tests.Middleware;

[TestFixture]
public class RequestBodyBufferingMiddlewareTests
{
    private RequestBodyBufferingMiddleware _middleware;
    private Mock<RequestDelegate> _nextMock;
    private HttpContext _httpContext;

    [SetUp]
    public void SetUp()
    {
        _nextMock = new Mock<RequestDelegate>();
        _httpContext = new DefaultHttpContext();

        _middleware = new RequestBodyBufferingMiddleware(_nextMock.Object);
    }

    [Test]
    public async Task InvokeAsync_EnablesRequestBodyBuffering()
    {
        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        _nextMock.Verify(x => x(_httpContext), Times.Once);
        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);
    }

    [Test]
    public async Task InvokeAsync_WithRequestBody_CallsNextDelegate()
    {
        const string testContent = "Test request body content";
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(testContent));
        _httpContext.Request.ContentLength = testContent.Length;
        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        _nextMock.Verify(x => x(_httpContext), Times.Once);
        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);
    }

    [Test]
    public async Task InvokeAsync_WithEmptyRequestBody_CallsNextDelegate()
    {
        _httpContext.Request.Body = new MemoryStream();
        _httpContext.Request.ContentLength = 0;
        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        _nextMock.Verify(x => x(_httpContext), Times.Once);
        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);
    }

    [Test]
    public async Task InvokeAsync_WhenNextDelegateThrows_PropagatesException()
    {
        var expectedException = new InvalidOperationException("Test exception");
        _nextMock.Setup(x => x(_httpContext)).ThrowsAsync(expectedException);

        InvalidOperationException exception = null!;
        try
        {
            await _middleware.InvokeAsync(_httpContext);
        }
        catch (InvalidOperationException ex)
        {
            exception = ex;
        }

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.Message, Is.EqualTo(expectedException.Message));
        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);
    }

    [Test]
    public async Task InvokeAsync_AllowsMultipleReadsFromRequestBody()
    {
        const string testContent = "Test content for multiple reads";
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(testContent));
        _httpContext.Request.ContentLength = testContent.Length;

        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);

        _httpContext.Request.Body.Position = 0;
        using var reader1 = new StreamReader(_httpContext.Request.Body, leaveOpen: true);
        var firstRead = await reader1.ReadToEndAsync();

        _httpContext.Request.Body.Position = 0;
        using var reader2 = new StreamReader(_httpContext.Request.Body);
        var secondRead = await reader2.ReadToEndAsync();

        Assert.That(firstRead, Is.EqualTo(testContent));
        Assert.That(secondRead, Is.EqualTo(testContent));
    }

    [Test]
    public async Task InvokeAsync_WithLargeRequestBody_HandlesCorrectly()
    {
        var largeContent = new string('A', 10000);
        _httpContext.Request.Body = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(largeContent));
        _httpContext.Request.ContentLength = largeContent.Length;
        _nextMock.Setup(x => x(_httpContext)).Returns(Task.CompletedTask);

        await _middleware.InvokeAsync(_httpContext);

        _nextMock.Verify(x => x(_httpContext), Times.Once);
        Assert.That(_httpContext.Request.Body.CanSeek, Is.True);
        Assert.That(_httpContext.Request.Body.Length, Is.EqualTo(largeContent.Length));
    }
}