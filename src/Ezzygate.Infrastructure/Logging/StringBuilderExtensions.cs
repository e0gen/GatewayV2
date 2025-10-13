using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ezzygate.Infrastructure.Extensions;
using Serilog.Events;

namespace Ezzygate.Infrastructure.Logging;

public static class StringBuilderExtensions
{
    public static void AddExceptions(this StringBuilder sb, LogEvent logEvent)
    {
        if (logEvent.Exception == null) return;
        var curEx = logEvent.Exception;
        while (curEx != null)
        {
            if (curEx != logEvent.Exception)
                sb.AppendLine("----------------------INNER-----------------");
            sb.AppendLine(curEx.ToString());
            curEx = curEx.InnerException;
        }
    }

    public static void AddMoreInfo(this StringBuilder sb, LogEvent logEvent)
    {
        sb.AppendLine(logEvent.GetMoreInfo());
    }

    public static void AddHttpCode(this StringBuilder sb, LogEvent logEvent)
    {
        var httpCode = logEvent.GetHttpCode();
        if (!string.IsNullOrEmpty(httpCode))
            sb.AppendLine("HTTP Code: {httpCode}");
    }

    public static void AddWebContextInfo(this StringBuilder sb, IServiceProvider serviceProvider)
    {
        try
        {
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var context = httpContextAccessor?.HttpContext;

            if (context?.Request == null) return;

            sb.AppendLine();
            sb.Append($"Authentication: {context.User?.Identity?.Name ?? ""} \n");
            sb.Append($"Url: {context.Request.Scheme}://{context.Request.Host}{context.Request.Path} \n");
            sb.Append($"Client IP: {context.GetRemoteIP()} \n");

            sb.AppendLine();
            sb.AppendLine("Headers:");
            if (context.Request.Headers is { Count: > 0 })
            {
                foreach (var header in context.Request.Headers)
                {
                    sb.AppendFormat("{0}: {1} \n", header.Key, header.Value);
                }
            }

            sb.AppendLine();
            sb.AppendLine("Query:");
            if (context.Request.Query is { Count: > 0 })
            {
                foreach (var query in context.Request.Query)
                {
                    if (query.Key != null)
                    {
                        if (query.Key.ToLower().Contains("cc") || query.Key.ToLower().Contains("card") ||
                            query.Key.ToLower().Contains("password"))
                            sb.Append($"{query.Key}: ---- \n");
                        else
                            sb.Append($"{query.Key}: {query.Value} \n");
                    }
                }
            }

            sb.AppendLine();
            sb.AppendLine("Form:");
            if (context.Request.HasFormContentType && context.Request.Form is { Count: > 0 })
            {
                foreach (var form in context.Request.Form)
                {
                    if (form.Key == null) continue;
                    if (form.Key.Contains("cc", StringComparison.CurrentCultureIgnoreCase)
                        || form.Key.Contains("card", StringComparison.CurrentCultureIgnoreCase)
                        || form.Key.Contains("password", StringComparison.CurrentCultureIgnoreCase))
                        sb.Append($"{form.Key}: ---- \n");
                    else
                        sb.Append($"{form.Key}: {form.Value} \n");
                }
            }

            // user info
            sb.AppendLine();
            sb.AppendLine("User:");
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                sb.AppendLine($"UserName: {context.User.Identity.Name}");
                foreach (var claim in context.User.Claims)
                    sb.AppendLine($"{claim.Type}: {claim.Value}");
            }
        }
        catch
        {
            sb.AppendLine("Failed to get web context info");
        }
    }

    public static void AddAssembliesInfo(this StringBuilder sb)
    {
        try
        {
            sb.AppendLine("Assemblies:");
            sb.AppendLine($"Executing Assembly: {GetInfo(Assembly.GetExecutingAssembly())}");
            sb.AppendLine($"Entry Assembly: {GetInfo(Assembly.GetEntryAssembly())}");
            sb.AppendLine($"Calling Assembly: {GetInfo(Assembly.GetCallingAssembly())}");
        }
        catch
        {
            sb.AppendLine("Failed to get assemblies info");
        }
    }

    private static string GetInfo(Assembly? assembly)
    {
        if (assembly == null) return string.Empty;

        try
        {
            var date = File.GetLastWriteTime(assembly.Location);
            var info = $"Name: '{assembly.GetName()}', Modify date: '{date}'";
            return info;
        }
        catch
        {
            return $"Name: '{assembly.GetName()}'";
        }
    }
}