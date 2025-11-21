using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Transactions;
using Ezzygate.Integrations.Extensions;

namespace Ezzygate.Integrations.Api;

public abstract class ApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    protected ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected abstract string GetBaseUrl(bool test);

    protected virtual void ConfigureRequest(HttpRequestMessage request, TransactionContext ctx, HttpMethod method,
        string path, string bodyJson)
    {
    }

    protected virtual void ConfigureClient(HttpClient client, TransactionContext ctx)
    {
    }

    protected ApiResult<TResult> MakeRequestInternal<TResult>(
        HttpMethod method, string path, object bodyJson, TransactionContext ctx)
        where TResult : class
    {
        return MakeRequestInternalAsync<TResult>(method, path, bodyJson, ctx).GetAwaiter().GetResult();
    }

    protected async Task<ApiResult<TResponse>> MakeRequestInternalAsync<TResponse>(
        HttpMethod method, string path, object body, TransactionContext ctx)
        where TResponse : class
    {
        var client = _httpClientFactory.CreateClient();

        ConfigureClient(client, ctx);

        var bodyJson = method == HttpMethod.Post ? body.Serialize() : string.Empty;

        var baseUrl = GetBaseUrl(ctx.IsTestTerminal);
        var httpBaseUrl = new Uri(baseUrl);
        var httpRequestUrl = new Uri(httpBaseUrl, path);

        using var request = new HttpRequestMessage(method, httpRequestUrl);

        ConfigureRequest(request, ctx, method, path, bodyJson);

        if (method == HttpMethod.Post)
            request.Content = new StringContent(bodyJson, Encoding.UTF8, "application/json");

        var response = await client.SendAsync(request).ConfigureAwait(false);
        var responseJson = await response.Content.ReadAsStringAsync();

        TResponse? tResponse = null;
        Exception? tException = null;
        try
        {
            tResponse = responseJson.Deserialize<TResponse>();
        }
        catch (Exception ex)
        {
            tException = ex;
        }

        bodyJson = body.MaskIfSensitive().Serialize();

        return new ApiResult<TResponse>
        {
            StatusCode = (int)response.StatusCode,
            Response = tResponse,
            ResponseJson = responseJson,
            RequestJson = bodyJson,
            Path = path,
            Exception = tException
        };
    }
}