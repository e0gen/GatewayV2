namespace Ezzygate.Domain.Models;

public class Account
{
    public int AccountId { get; set; }
    public string? AccountNumber { get; set; }
    public string? Name { get; set; }
    public string? HashKey { get; set; }
}