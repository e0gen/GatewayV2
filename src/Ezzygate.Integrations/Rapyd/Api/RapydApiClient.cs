using Ezzygate.Domain.Enums;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;
using Ezzygate.Integrations.Extensions;
using Ezzygate.Integrations.Rapyd.Models;

namespace Ezzygate.Integrations.Rapyd.Api;

public sealed class RapydApiClient : ApiClient, IRapydApiClient
{
    private const string BaseUrlTest = "https://sandboxapi.rapyd.net";
    private const string BaseUrlProd = "https://api.rapyd.net";

    public RapydApiClient(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
    }

    protected override string GetBaseUrl(bool test) => test ? BaseUrlTest : BaseUrlProd;

    protected override void ConfigureRequest(HttpRequestMessage request, TransactionContext ctx, HttpMethod method,
        string path, string bodyJson)
    {
        var secretKey = ctx.Terminal?.AuthenticationCode1;
        var accessKey = ctx.Terminal?.AccountId;
        
        if(string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
            throw new Exception("Missing credentials configuration in Terminal");

        var salt = RapydServices.GenerateRandomString(8);
        var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var signature = RapydServices.GetRequestHeaderSignature(method.Method, path, salt, timestamp, bodyJson,
            accessKey, secretKey);

        request.Headers.Add("salt", salt);
        request.Headers.Add("timestamp", timestamp.ToString());
        request.Headers.Add("signature", signature);
        request.Headers.Add("access_key", accessKey);
    }

    public async Task<ApiResult<RapydResponse<PaymentData>>> ProcessRequestAsync(TransactionContext ctx,
        string midCountry, bool capture, PaymentMethodEnum method, CancellationToken cancellationToken = default)
    {
        const string path = "/v1/payments";
        var methodType = method == PaymentMethodEnum.CCMastercard
            ? $"{midCountry}_mastercard_card"
            : $"{midCountry}_visa_card";

        var body = RapydApiFactory.CreateProcessRequest(ctx, capture, methodType);

        return await MakeRequestInternalAsync<RapydResponse<PaymentData>>(HttpMethod.Post, path, body, ctx, cancellationToken);
    }

    public async Task<ApiResult<RapydResponse<RefundData>>> RefundRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        const string path = "/v1/refunds";

        var body = RapydApiFactory.CreateRefundRequest(ctx);

        return await MakeRequestInternalAsync<RapydResponse<RefundData>>(HttpMethod.Post, path, body, ctx, cancellationToken);
    }

    public async Task<ApiResult<RapydResponse<PaymentData>>> CaptureRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var path = $"/v1/payments/{ctx.ApprovalNumber}/capture";

        var body = RapydApiFactory.CreateCaptureRequest(ctx);

        return await MakeRequestInternalAsync<RapydResponse<PaymentData>>(HttpMethod.Post, path, body, ctx, cancellationToken);
    }

    public async Task<ApiResult<RapydResponse<PaymentData>>> StatusRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var path = $"/v1/payments/{ctx.LocatedTrx.ApprovalNumber}";

        return await MakeRequestInternalAsync<RapydResponse<PaymentData>>(HttpMethod.Get, path, null, ctx, cancellationToken);
    }

    // public string GetMethodsByCountry(TransactionContext ctx)
    // {
    //     const string path = "/v1/payment_methods/countries/" + "PL";
    //
    //     return MakeRequestInternal(HttpMethod.Get, path, null, ctx);
    // }

    public RapydResponse<PaymentData>? RestorePaymentDataResponse(string responseBody)
    {
        return responseBody.Deserialize<RapydResponse<PaymentData>>();
    }
}