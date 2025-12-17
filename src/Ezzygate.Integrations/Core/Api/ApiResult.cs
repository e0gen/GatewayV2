namespace Ezzygate.Integrations.Core.Api;

public class ApiResult<T>
{
    public int StatusCode;
    public T? Response;
    public required string Path;
    public string? RequestJson;
    public string? ResponseJson;
    public Exception? Exception;
}