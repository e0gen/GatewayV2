using System.Net;
using System.Text.Json;
using Ezzygate.Application.Models;
using Ezzygate.WebApi.Models;
using Serilog;

namespace Ezzygate.WebApi.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception in WebApi");

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = _environment.IsDevelopment()
            ? exception.ToString()
            : "General error, see logs.";

        var controllerName = context.Request.RouteValues["controller"]?.ToString();
        object result;

        if (controllerName == "Integration")
        {
            result = new IntegrationResult { Code = "520", Message = error };
        }
        else
        {
            result = new Response(Result.GeneralError, error);
        }

        var jsonResponse = JsonSerializer.Serialize(result, _jsonSerializerOptions);

        await context.Response.WriteAsync(jsonResponse);
    }
}