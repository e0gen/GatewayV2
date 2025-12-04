using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ezzygate.Application.Integrations;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Utilities;
using Ezzygate.WebApi.Dtos;

namespace Ezzygate.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IntegrationSecurityFilterAttribute : FilterBase
{
    private const string SignatureKey = "signature";
    private const string SignatureSalt = "c3722f2e-d476-40a5-abea-b4c5b66a8891";

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validationResult = ValidateModel(context);
        if (!validationResult)
            return;

        var httpContext = context.HttpContext;
        var request = httpContext.Request;

        var clientSignature = request.GetHeaderValue(SignatureKey);
        if (string.IsNullOrEmpty(clientSignature))
        {
            context.Result = new OkObjectResult(new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SignatureRequired) });
            return;
        }

        var content = await request.ReadBodyAsStringAsync();
        var serverSignature = HashUtils.ComputeSha256Hash(content + SignatureSalt);

        if (clientSignature != serverSignature)
        {
            context.Result = new OkObjectResult(new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SignatureMismatch) });
            return;
        }

        if (!httpContext.Connection.IsLocal() && !request.IsHttps)
        {
            context.Result = new OkObjectResult(new IntegrationResult { Code = "520", Message = nameof(ResultEnum.SslRequired) });
            return;
        }

        await next();
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