namespace Ezzygate.WebApi.Extensions;

public static class HttpRequestExtensions
{
    public static string GetRequestContent(this HttpRequest request)
    {
        if (!request.Body.CanSeek) return string.Empty;

        request.Body.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(request.Body, leaveOpen: true);
        return reader.ReadToEnd();
    }

    public static string? FirstOrDefault(this IHeaderDictionary headers, string headerName)
    {
        return headers.TryGetValue(headerName, out var value) && value.Count != 0 ? value.First() : null;
    }
}