using Asp.Versioning.ApiExplorer;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Ezzygate.WebApi.Configuration;
using Ezzygate.WebApi.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ezzygate.WebApi.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("MerchantAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-Merchant-Auth",
                Description = """
                              **Ezzygate Merchant Authentication**

                              Enter your credentials in the format: `{merchant-number}:{hash}`

                              **Required headers will be auto-generated:**
                              • `merchant-number`
                              • `request-id`
                              • `signature`
                              """
            });

            options.AddSecurityDefinition("IntegrationAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-Integration-Auth",
                Description = """
                              **Ezzygate Integration Authentication**

                              Enter your signature salt in the format: `{salt}`

                              **Required signature header will be auto-generated:**
                              """
            });

            options.OperationFilter<MerchantSecurityOperationFilter>();
            options.OperationFilter<MerchantAuthDocumentationFilter>();
            options.OperationFilter<IntegrationSecurityOperationFilter>();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"Ezzygate WebAPI {description.GroupName.ToUpperInvariant()}");
            }

            options.RoutePrefix = "swagger";
            options.DocumentTitle = "Ezzygate WebAPI Documentation";

            options.InjectJavascript("/js/swagger-auth-interceptor.js");
        });

        return app;
    }
}

[UsedImplicitly]
public class MerchantSecurityOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasMerchantSecurity = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<MerchantSecurityFilterAttribute>()
            .Any()
            || context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<MerchantSecurityFilterAttribute>()
                .Any() == true;

        if (!hasMerchantSecurity)
            return;

        operation.Security ??= new List<OpenApiSecurityRequirement>();
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "MerchantAuth"
                    }
                }, []
            }
        });
    }
}

[UsedImplicitly]
public class MerchantAuthDocumentationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasMerchantAuth = operation.Security?
            .Any(req => req.Keys.Any(k => k.Reference?.Id == "MerchantAuth")) ?? false;

        if (!hasMerchantAuth)
            return;

        const string authNote = """
                                **Merchant Authentication Required**

                                Click **Authorize** above to enter credentials in format: `merchant-number:hash`
                                """;

        operation.Description = (operation.Description ?? "") + authNote;
    }
}

[UsedImplicitly]
public class IntegrationSecurityOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasIntegrationSecurity = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<IntegrationSecurityFilterAttribute>()
            .Any()
            || context.MethodInfo.DeclaringType?
                .GetCustomAttributes(true)
                .OfType<IntegrationSecurityFilterAttribute>()
                .Any() == true;

        if (!hasIntegrationSecurity)
            return;

        operation.Security ??= new List<OpenApiSecurityRequirement>();
        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "IntegrationAuth"
                    }
                }, []
            }
        });
    }
}