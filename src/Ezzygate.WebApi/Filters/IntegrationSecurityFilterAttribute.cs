using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ezzygate.Application.Integrations;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Models;
using Ezzygate.WebApi.Utils;

namespace Ezzygate.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IntegrationSecurityFilterAttribute : FilterBase
{
    private const string SignatureKey = "signature";
    private const string SignatureSalt = "c3722f2e-d476-40a5-abea-b4c5b66a8891";


    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var validationResult = ValidateModel(context);
        if (!validationResult)
            return;

        var httpContext = context.HttpContext;
        var request = httpContext.Request;

        var clientSignature = request.Headers.FirstOrDefault(SignatureKey);
        if (string.IsNullOrEmpty(clientSignature))
        {
            var result = new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SignatureRequired) };
            context.Result = new OkObjectResult(result);
            return;
        }

        var content = request.GetRequestContent();
        var serverSignature = HashUtils.ComputeSha256Hash(content + SignatureSalt);

        if (clientSignature != serverSignature)
        {
            var result = new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SignatureMismatch) };
            context.Result = new OkObjectResult(result);
            return;
        }

        // SSL check for non-local requests
        if (!httpContext.Connection.RemoteIpAddress?.IsIPv4MappedToIPv6 == true &&
            !httpContext.Request.IsHttps)
        {
            var result = new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SslRequired) };
            context.Result = new OkObjectResult(result);
            return;
        }

        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult { Value: not null } objectResult)
        {
            var content = JsonSerializer.Serialize(objectResult.Value);
            var signature = HashUtils.ComputeSha256Hash(content + SignatureSalt);

            context.HttpContext.Response.Headers[SignatureKey] = signature;
        }

        base.OnActionExecuted(context);
    }
}