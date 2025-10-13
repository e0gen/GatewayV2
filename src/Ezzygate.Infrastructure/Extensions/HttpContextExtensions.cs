using Microsoft.AspNetCore.Http;

namespace Ezzygate.Infrastructure.Extensions;

public static class HttpContextExtensions
{
    public static string GetClientIP(this HttpContext? context) => GetClientIPInternal(context);
    public static string GetRemoteIP(this HttpContext? context) => context?.Connection?.RemoteIpAddress?.ToString() ?? "";
    public static string GetLocalIP(this HttpContext? context) => context?.Connection?.LocalIpAddress?.ToString() ?? "";
    public static string GetRemoteUser(this HttpContext? context) => context?.User?.Identity?.Name ?? "";
    public static string GetServerPort(this HttpContext? context) => context?.Request?.Host.Port?.ToString() ?? "";
    public static string GetScriptName(this HttpContext? context) => context?.Request?.Path.Value ?? "";
    public static string GetRequestQueryString(this HttpContext? context) => context?.Request?.QueryString.Value ?? "";
    public static string GetVirtualPath(this HttpContext? context) => context?.Request?.Path.Value ?? "";
    public static string GetPhysicalPath(this HttpContext? context) => context?.Request?.PathBase.Value ?? "";
    public static string GetRequestForm(this HttpContext? context)
    {
        try
        {
            if (context?.Request?.HasFormContentType == true && context.Request.Form != null)
                return string.Join("&", context.Request.Form.Select(f => $"{f.Key}={f.Value}"));
        }
        catch { }
        return "";
    }

    private static string GetClientIPInternal(HttpContext? context)
    {
        if (context?.Request?.Headers != null)
        {
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
                return forwardedFor.Split(',')[0].Trim();

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
                return realIp;
        }

        return context?.Connection?.RemoteIpAddress?.ToString() ?? "";
    }
}