namespace Ezzygate.Domain.Models;

public class DebitCompany
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? IntegrationTag { get; set; }
}