using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.Paysafe.Api;

public sealed class PaysafeApiClient : ApiClient, IPaysafeApiClient
{
    private const string BaseUrlTest = "https://api.test.paysafe.com";
    private const string BaseUrlProd = "https://api.paysafe.com";

    public PaysafeApiClient(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
    }

    protected override string GetBaseUrl(bool test) => test ? BaseUrlTest : BaseUrlProd;

    protected override void ConfigureClient(HttpClient client, TransactionContext ctx)
    {
        var clientSecret = ctx.Terminal?.AuthenticationCode1;
        var clientId = ctx.Terminal?.AccountId;
        
        if(string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(clientId))
            throw new Exception("Missing credentials configuration in Terminal");

        client.AddBasicAuth(clientId, clientSecret);
    }

    public async Task<ApiResult<CreatePaymentHandlerResponse>> CreatePaymentHandlerAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        const string path = "/paymenthub/v1/paymenthandles";

        var request = PaysafeApiFactory.CreatePaymentHandlerRequest(ctx);

        return await MakeRequestInternalAsync<CreatePaymentHandlerResponse>(HttpMethod.Post, path, request, ctx, cancellationToken);
    }

    public async Task<ApiResult<PaymentResponse>> ProcessPaymentAsync(TransactionContext ctx, bool onlyAuthorize, CancellationToken cancellationToken = default)
    {
        const string path = "/paymenthub/v1/payments";

        var request = PaysafeApiFactory.CreatePaymentRequest(ctx, onlyAuthorize);

        return await MakeRequestInternalAsync<PaymentResponse>(HttpMethod.Post, path, request, ctx, cancellationToken);
    }

    public async Task<ApiResult<SettlementResponse>> ProcessSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var paymentId = ctx.ApprovalNumber;
        var path = $"/paymenthub/v1/payments/{paymentId}/settlements";

        var request = PaysafeApiFactory.CreateSettlementRequest(ctx);

        return await MakeRequestInternalAsync<SettlementResponse>(HttpMethod.Post, path, request, ctx, cancellationToken);
    }

    public async Task<ApiResult<VoidAuthorizationResponse>> VoidAuthorizationAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var paymentId = ctx.ApprovalNumber;
        var path = $"/paymenthub/v1/payments/{paymentId}/voidauths";

        var request = PaysafeApiFactory.CreateVoidAuthorizationRequest(ctx);

        return await MakeRequestInternalAsync<VoidAuthorizationResponse>(HttpMethod.Post, path, request, ctx, cancellationToken);
    }

    public async Task<ApiResult<SettlementResponse>> GetSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var settlementId = ctx.ApprovalNumber;
        var path = $"/paymenthub/v1/settlements/{settlementId}";

        return await MakeRequestInternalAsync<SettlementResponse>(HttpMethod.Get, path, null!, ctx, cancellationToken);
    }

    public async Task<ApiResult<EmptyResponse>> CancelSettlementAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var settlementId = ctx.ApprovalNumber;
        var path = $"/paymenthub/v1/settlements/{settlementId}";

        var request = PaysafeApiFactory.CreateCancelSettlementRequest();

        return await MakeRequestInternalAsync<EmptyResponse>(HttpMethod.Put, path, request, ctx, cancellationToken);
    }

    public async Task<ApiResult<RefundResponse>> ProcessRefundAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var settlementId = ctx.ApprovalNumber;
        var path = $"/paymenthub/v1/settlements/{settlementId}/refunds";

        var request = PaysafeApiFactory.CreateRefundRequest(ctx);

        return await MakeRequestInternalAsync<RefundResponse>(HttpMethod.Post, path, request, ctx, cancellationToken);
    }
}

