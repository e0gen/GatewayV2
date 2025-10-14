namespace Ezzygate.Infrastructure.Configuration;

public class IntegrationSettings
{
    public const string SectionName = "IntegrationSettings";
    public bool DisablePostRedirectUrl { get; set; } = false;
}