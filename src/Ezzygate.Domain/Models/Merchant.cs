namespace Ezzygate.Domain.Models;

public class Merchant
{
    public int Id { get; set; }
    public string CustomerNumber { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? HashKey { get; set; }
    public int? AccountId { get; set; }
    public bool IsActive { get; set; }
    
    // Navigation properties
    public Account? Account { get; set; }
}