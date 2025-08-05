namespace Ezzygate.Domain.Models;

public class Terminal
{
    public int Id { get; set; }
    public string TerminalNumber { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string AccountSubId { get; set; } = string.Empty;
    public string TerminalNumber3D { get; set; } = string.Empty;
    public string AccountId3D { get; set; } = string.Empty;
    public string AccountSubId3D { get; set; } = string.Empty;
    public bool IsTestTerminal { get; set; }
    public bool Enable3DSecure { get; set; }
    public byte DebitCompanyId { get; set; }
    public string? AuthenticationCode1 { get; set; } = string.Empty;
    public string? AuthenticationCode3D { get; set; } = string.Empty;
    public byte[]? AccountPassword256 { get; set; }
    public byte[]? AccountPassword3D256 { get; set; }
}