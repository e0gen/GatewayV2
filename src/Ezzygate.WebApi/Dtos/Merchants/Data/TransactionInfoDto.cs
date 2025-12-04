namespace Ezzygate.WebApi.Dtos.Merchants.Data;

public class TransactionInfoDto
{
    public int Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? ApprovalCode { get; set; }
    public string? Card { get; set; }
    public DateTime InsertDate { get; set; }
    public string? Currency { get; set; }
    public int Installments { get; set; }
    public string? Comment { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CardholderName { get; set; }
    public string? PersonalNumber { get; set; }
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public string? DebitReferenceCode { get; set; }
    public string? OrderId { get; set; }
}