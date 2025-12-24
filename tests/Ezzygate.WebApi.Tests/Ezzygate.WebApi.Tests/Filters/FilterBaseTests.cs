using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Ezzygate.Application.Integrations;
using Ezzygate.WebApi.Dtos;
using Ezzygate.WebApi.Filters;

namespace Ezzygate.WebApi.Tests.Filters;

[TestFixture]
public class FilterBaseTests
{
    private Mock<IServiceProvider> _serviceProviderMock;
    private ActionExecutingContext _actionExecutingContext;
    private TestFilterBase _filter;
    private DefaultHttpContext _httpContext;

    [SetUp]
    public void SetUp()
    {
        _serviceProviderMock = new Mock<IServiceProvider>();
        _httpContext = new DefaultHttpContext
        {
            RequestServices = _serviceProviderMock.Object
        };

        var actionDescriptor = new ControllerActionDescriptor();
        var routeData = new RouteData();

        var actionContext = new ActionContext(_httpContext, routeData, actionDescriptor);
        _actionExecutingContext = new ActionExecutingContext(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object?>(), new Mock<ControllerBase>().Object);

        _filter = new TestFilterBase();
    }

    [Test]
    public void ValidateModel_WithValidModelState_ReturnsTrue()
    {
        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public void ValidateModel_WithInvalidModelState_ReturnsFalseAndSetsIntegrationResult()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "Integration";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<IntegrationResult>());

        var integrationResult = objectResult.Value as IntegrationResult;
        Assert.That(integrationResult!.Code, Is.EqualTo("520"));
        Assert.That(integrationResult.Message, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithInvalidModelState_ReturnsFalseAndSetsRegularResponse()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<Response>());

        var response = objectResult.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequest)));
        Assert.That(response.Data, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithMultipleModelStateErrors_CollectsAllErrors()
    {
        _actionExecutingContext.ModelState.AddModelError("Field1", "Error1");
        _actionExecutingContext.ModelState.AddModelError("Field2", "Error2");
        _actionExecutingContext.ModelState.AddModelError("Field3", "Error3");
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void ValidateModel_WithModelStateErrorWithException_CollectsExceptionMessage()
    {
        var exception = new InvalidOperationException("Test exception message");
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var modelMetadata = modelMetadataProvider.GetMetadataForType(typeof(object));
        _actionExecutingContext.ModelState.AddModelError("Field1", exception, modelMetadata);
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void ValidateModel_WithNullControllerName_ReturnsRegularResponse()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = null;

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<Response>());

        var response = objectResult.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequest)));
        Assert.That(response.Data, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithEmptyControllerName_ReturnsRegularResponse()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<Response>());

        var response = objectResult.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequest)));
        Assert.That(response.Data, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithWhitespaceControllerName_ReturnsRegularResponse()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "   ";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<Response>());

        var response = objectResult.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequest)));
        Assert.That(response.Data, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithCaseInsensitiveIntegrationController_ReturnsIntegrationResult()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "integration";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<Response>());
        var response = objectResult.Value as Response;
        Assert.That(response!.Result, Is.EqualTo(nameof(ResultEnum.InvalidRequest)));
        Assert.That(response.Data, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithMixedCaseIntegrationController_ReturnsIntegrationResult()
    {
        _actionExecutingContext.ModelState.AddModelError("TestField", "Test error");
        _actionExecutingContext.RouteData.Values["controller"] = "Integration";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);
        Assert.That(_actionExecutingContext.Result, Is.TypeOf<ObjectResult>());

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
        Assert.That(objectResult.Value, Is.TypeOf<IntegrationResult>());

        var integrationResult = objectResult.Value as IntegrationResult;
        Assert.That(integrationResult!.Code, Is.EqualTo("520"));
        Assert.That(integrationResult.Message, Is.EqualTo("General error, see logs"));
    }

    [Test]
    public void ValidateModel_WithEmptyModelState_ReturnsTrue()
    {
        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.True);
        Assert.That(_actionExecutingContext.Result, Is.Null);
    }

    [Test]
    public void ValidateModel_WithModelStateErrorContainingMultipleErrors_CollectsAllErrors()
    {
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Field1", "Error1");
        modelState.AddModelError("Field1", "Error2");
        modelState.AddModelError("Field2", "Error3");

        foreach (var key in modelState.Keys)
        {
            foreach (var error in modelState[key]!.Errors)
                _actionExecutingContext.ModelState.AddModelError(key, error.ErrorMessage);
        }
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void ValidateModel_WithModelStateErrorWithNullErrorMessage_HandlesGracefully()
    {
        _actionExecutingContext.ModelState.AddModelError("Field1", "");
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public void ValidateModel_WithModelStateErrorWithNullException_HandlesGracefully()
    {
        var modelError = new ModelError("Test error");
        _actionExecutingContext.ModelState.AddModelError("Field1", modelError.ErrorMessage);
        _actionExecutingContext.RouteData.Values["controller"] = "Data";

        var result = _filter.TestValidateModel(_actionExecutingContext);

        Assert.That(result, Is.False);
        Assert.That(_actionExecutingContext.Result, Is.Not.Null);

        var objectResult = _actionExecutingContext.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult!.StatusCode, Is.EqualTo(500));
    }

    private class TestFilterBase : FilterBase
    {
        public bool TestValidateModel(ActionExecutingContext context)
        {
            return ValidateModel(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }

        public override void OnActionExecuted(ActionExecutedContext context) { }
    }
}