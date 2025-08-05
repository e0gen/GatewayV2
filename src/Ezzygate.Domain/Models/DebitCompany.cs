namespace Ezzygate.Domain.Models;

public class DebitCompany
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}