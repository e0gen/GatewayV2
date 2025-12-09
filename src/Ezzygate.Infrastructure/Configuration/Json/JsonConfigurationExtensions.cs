using Microsoft.Extensions.Configuration;

namespace Ezzygate.Infrastructure.Configuration.Json;

public static class JsonConfigurationExtensions
{
    private const string InfrastructureJsonConfigFile = "Ezzygate.Infrastructure.config.json";

    public static IConfigurationBuilder AddInfrastructureConfigurationSource(
        this IConfigurationBuilder builder,
        string? basePath = null)
    {
        var jsonConfigPath = ResolveConfigPath(InfrastructureJsonConfigFile, basePath);

        if (File.Exists(jsonConfigPath))
            return builder.AddJsonFile(jsonConfigPath, optional: false, reloadOnChange: true);

        throw new FileNotFoundException($"Infrastructure configuration file not found: {jsonConfigPath}");
    }

    private static string ResolveConfigPath(string fileName, string? basePath)
    {
        var configPath = string.IsNullOrEmpty(basePath) ? fileName : Path.Combine(basePath, fileName);

        if (File.Exists(configPath) || !string.IsNullOrEmpty(basePath))
            return configPath;
        var appBasePath = AppContext.BaseDirectory;
        var appConfigPath = Path.Combine(appBasePath, fileName);

        return File.Exists(appConfigPath) ? appConfigPath : configPath;
    }
}