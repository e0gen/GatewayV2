using Asp.Versioning;
using Ezzygate.Application;
using Ezzygate.Infrastructure.Configuration.Json;
using Ezzygate.Infrastructure.Extensions;
using Ezzygate.Infrastructure.Logging;
using Ezzygate.Integrations.Extensions;
using Ezzygate.WebApi.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(3, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("X-Version"),
        new HeaderApiVersionReader("api-version")
    );
}).AddApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Configuration.AddInfrastructureConfigurationSource();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);
builder.Services.AddIntegrations();
builder.Services.AddApplication();

var app = builder.Build();

var loggerConfig = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.BllLogDatabase(app.Services);

Log.Logger = loggerConfig.CreateLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<RequestBodyBufferingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => new
{
    Message = "Ezzygate WebAPI is running",
    Version = "1.0.0",
    Timestamp = DateTime.UtcNow
}).WithName("Root");

try
{
    Log.Information("Starting Ezzygate WebAPI host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}