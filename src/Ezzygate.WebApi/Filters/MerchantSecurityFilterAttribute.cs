using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ezzygate.Infrastructure.Repositories.Interfaces;
using Ezzygate.WebApi.Extensions;
using Ezzygate.WebApi.Models;
using Ezzygate.WebApi.Utils;

namespace Ezzygate.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MerchantSecurityFilterAttribute : FilterBase
{
    public MerchantSecurityFilterAttribute() : this(true)
    {
    }

    public MerchantSecurityFilterAttribute(bool requireSignature)
    {
        _requireSignature = requireSignature;
    }

    private readonly bool _requireSignature;
    private const string SignatureKey = "signature";
    private const string RequestIdKey = "request-id";
    private const string MerchantNumberKey = "merchant-number";

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validationResult = ValidateModel(context);
        if (!validationResult)
            return;

        if (_requireSignature)
        {
            var clientSignature = context.HttpContext.Request.Headers.FirstOrDefault(SignatureKey);
            if (string.IsNullOrEmpty(clientSignature))
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.SignatureRequired));
                return;
            }

            var clientRequestId = context.HttpContext.Request.Headers.FirstOrDefault(RequestIdKey);
            if (string.IsNullOrEmpty(clientRequestId))
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.RequestIdRequired));
                return;
            }

            var merchantNumber = context.HttpContext.Request.Headers.FirstOrDefault(MerchantNumberKey);
            if (string.IsNullOrEmpty(merchantNumber))
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.MerchantNumberRequired));
                return;
            }

            var merchantRepository = context.HttpContext.RequestServices.GetRequiredService<IMerchantRepository>();
            var requestIdRepository = context.HttpContext.RequestServices.GetRequiredService<IRequestIdRepository>();

            var merchant = await merchantRepository.GetByMerchantNumberAsync(merchantNumber);
            context.HttpContext.SetMerchant(merchant);

            // merchant check
            if (merchant == null)
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.MerchantNotFound));
                return;
            }

            // request id check
            if (!Guid.TryParse(clientRequestId, out _))
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.InvalidRequestId));
                return;
            }

            if (await requestIdRepository.IsRequestIdUsedAsync(clientRequestId))
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.DuplicateRequestId));
                return;
            }

            await requestIdRepository.MarkRequestIdAsUsedAsync(clientRequestId);

            // signature check
            var content = context.HttpContext.Request.GetRequestContent();
            var hashKey = merchant.Account?.HashKey ?? merchant.HashKey ?? "defaultHashKey";
            var serverSignature = HashUtils.ComputeSha256Hash(content + clientRequestId + hashKey);
            if (clientSignature != serverSignature)
            {
                context.Result = new BadRequestObjectResult(new Response(ResultEnum.SignatureMismatch));
                return;
            }
        }

        // ssl check
        if (!context.HttpContext.Connection.IsLocal() && !context.HttpContext.Request.IsHttps)
        {
            context.Result = new BadRequestObjectResult(new Response(ResultEnum.SslRequired));
            return;
        }

        await next();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult { Value: not null } objectResult)
        {
            var merchant = context.HttpContext.GetMerchant();

            var requestId = Guid.NewGuid().ToString();
            context.HttpContext.Response.Headers[RequestIdKey] = requestId;
            var content = JsonSerializer.Serialize(objectResult.Value);

            var hashKey = merchant?.Account?.HashKey ?? merchant?.HashKey ?? "defaultHashKey";
            var signature = HashUtils.ComputeSha256Hash(content + requestId + hashKey);
            context.HttpContext.Response.Headers[SignatureKey] = signature;
        }

        base.OnActionExecuted(context);
    }
}