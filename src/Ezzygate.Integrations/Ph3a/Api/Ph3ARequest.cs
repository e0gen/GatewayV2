namespace Ezzygate.Integrations.Ph3a.Api;

public record Ph3ARequest
{
    public string? CustomerIdNumber { get; set; }
    public string? ZipCode { get; set; }
    public string? Phone { get; set; }
} 