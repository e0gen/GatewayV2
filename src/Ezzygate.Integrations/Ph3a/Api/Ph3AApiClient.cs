using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Integrations.Core.Api;

// ReSharper disable StringLiteralTypo

namespace Ezzygate.Integrations.Ph3a.Api;

public class Ph3AApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _jsonOptions;

    private const string BaseUrl = "https://www.ph3a.com.br";

    private AuthData? _authData;

    public Ph3AApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };
    }

    public async Task AuthenticateAsync(string userName, CancellationToken cancellationToken = default)
    {
        const string path = "/datamind/Rest/Login";

        var body = Ph3AApiFactory.CreateLoginRequest(userName);

        var httpBaseUrl = new Uri(BaseUrl);
        var httpRequestUrl = new Uri(httpBaseUrl, path);

        using var client = _httpClientFactory.CreateClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, httpRequestUrl);
        request.Content = JsonContent.Create(body, options: _jsonOptions);
        using var response = await client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);

        _authData = loginResponse.NotNull().Data;
    }

    public async Task<ApiResult<AnalysisResponse>> GetQuizAnalysisAsync(Ph3ARequest requestDto,
        CancellationToken cancellationToken = default)
    {
        const string path = "/datamind/Rest/getQuizAnalysis";

        var request = Ph3AApiFactory.CreateAnalysisRequest(requestDto);

        return await MakeRequestInternalAsync<AnalysisResponse>(HttpMethod.Post, path, request, cancellationToken);
    }

    private async Task<ApiResult<TResponse>> MakeRequestInternalAsync<TResponse>(
        HttpMethod method,
        string path,
        object? body = null,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        if (_authData == null)
            throw new Exception("Client not authenticated. Please log in first.");

        var httpBaseUrl = new Uri(BaseUrl);
        var httpRequestUrl = new Uri(httpBaseUrl, path);

        using var client = _httpClientFactory.CreateClient();
        using var request = new HttpRequestMessage(method, httpRequestUrl);

        request.Headers.Add("userId", _authData.UserId);
        request.Headers.Add("token", _authData.Token);

        string? requestJson = null;
        TResponse? result = null;

        if (body is not null && (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Patch))
        {
            requestJson = JsonSerializer.Serialize(body, _jsonOptions);
            request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        }

        using var response = await client.SendAsync(request, cancellationToken);

        var responseJson = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
            result = JsonSerializer.Deserialize<TResponse>(responseJson, _jsonOptions);

        return new ApiResult<TResponse>
        {
            StatusCode = (int)response.StatusCode,
            Path = path,
            RequestJson = requestJson,
            ResponseJson = responseJson,
            Response = result
        };
    }
}