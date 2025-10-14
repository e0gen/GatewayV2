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

    public static async Task<string> GetRequestContentAsync(this HttpRequest request)
    {
        if (request.ContentLength is null or 0) return string.Empty;

        request.EnableBuffering();
        request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, leaveOpen: true);
        var content = await reader.ReadToEndAsync();
        request.Body.Position = 0;

        return content;
    }

    public static string? FirstOrDefault(this IHeaderDictionary headers, string headerName)
    {
        return headers.TryGetValue(headerName, out var value) && value.Count != 0 ? value.First() : null;
    }
}