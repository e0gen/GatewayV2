using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Core.Api;

namespace Ezzygate.Integrations.QrMoney.Api;

public sealed class QrMoneyApiClient : ApiClient, IQrMoneyApiClient
{
    private const string BaseUrl = "https://apay.qrmoneysolutions.com";

    public QrMoneyApiClient(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
    }

    protected override string GetBaseUrl(bool test) => BaseUrl;

    protected override void ConfigureRequest(HttpRequestMessage request, TransactionContext ctx, HttpMethod method,
        string path, string bodyJson)
    {
        var secretKey = ctx.Terminal?.AuthenticationCode1;

        if (string.IsNullOrEmpty(secretKey))
            throw new Exception("Missing credentials configuration in Terminal");

        request.Headers.Add("X-API-Key", secretKey);
    }

    public async Task<ApiResult<QrMoneyPaymentResponse>> PaymentRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var accountData = ctx.Terminal?.AccountId.Split('|') ?? [];
        if (accountData.Length < 2)
            throw new Exception("Invalid AccountId format. Expected: accountId|originDomain");

        var accountId = accountData[0];
        var originDomain = accountData[1];

        var path = $"/payments/h2h/{accountId}";
        var body = QrMoneyApiFactory.CreatePaymentRequest(ctx, originDomain);

        return await MakeRequestInternalAsync<QrMoneyPaymentResponse>(HttpMethod.Post, path, body, ctx, cancellationToken);
    }

    public async Task<ApiResult<QrMoneyStatusResponse>> StatusRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var path = $"/payment-requests/status/{ctx.LocatedTrx?.ApprovalNumber}";

        return await MakeRequestInternalAsync<QrMoneyStatusResponse>(HttpMethod.Get, path, null!, ctx, cancellationToken);
    }

    public async Task<ApiResult<EmptyResponse>> RefundRequestAsync(TransactionContext ctx, CancellationToken cancellationToken = default)
    {
        var path = $"/payments/h2h/refund/{ctx.LocatedTrx?.ApprovalNumber}";

        return await MakeRequestInternalAsync<EmptyResponse>(HttpMethod.Get, path, null!, ctx, cancellationToken);
    }
}