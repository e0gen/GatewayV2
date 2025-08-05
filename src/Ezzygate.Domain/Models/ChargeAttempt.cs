namespace Ezzygate.Domain.Models;

public class ChargeAttempt
{
    public int Id { get; set; }
    public string QueryString { get; set; } = string.Empty;
    public string RequestForm { get; set; } = string.Empty;
}