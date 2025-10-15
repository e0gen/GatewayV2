namespace Ezzygate.Infrastructure;

public class ApiResult<T>
{
    public int StatusCode { get; init; }
    public T? Response { get; init; }
    public required string Path { get; init; }
    public string? RequestJson { get; init; }
    public string? ResponseJson { get; init; }
}