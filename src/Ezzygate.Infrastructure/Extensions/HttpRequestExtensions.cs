using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Ezzygate.Infrastructure.Extensions;

public static class HttpRequestExtensions
{
    public static string? GetHeaderValue(this HttpRequest request, string headerName)
    {
        return request.Headers.TryGetValue(headerName, out var values)
            ? values.FirstOrDefault()
            : null;
    }

    public static string ReadBodyAsString(this HttpRequest request)
    {
        if (request.Body.CanSeek)
            request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = reader.ReadToEnd();

        if (request.Body.CanSeek)
            request.Body.Position = 0;

        return body;
    }

    public static async Task<string> ReadBodyAsStringAsync(this HttpRequest request)
    {
        if (request.Body.CanSeek)
            request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();

        if (request.Body.CanSeek)
            request.Body.Position = 0;

        return body;
    }

    public static HttpClient AddBasicAuth(this HttpClient client, string clientId, string clientSecret)
    {
        var authenticationString = $"{clientId}:{clientSecret}";
        var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

        return client;
    }

    public static string ToEncodedUrl(this string source, Encoding? encoding = null)
    {
        return encoding == null ? HttpUtility.UrlEncode(source) : HttpUtility.UrlEncode(source, encoding);
    }

    public static string ToDecodedUrl(this string source, Encoding? encoding = null)
    {
        return encoding == null ? HttpUtility.UrlDecode(source) : HttpUtility.UrlDecode(source, encoding);
    }
}