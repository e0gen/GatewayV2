namespace Ezzygate.Integrations.Core.Api;

public class ApiResult<T>
{
    public int StatusCode;
    public T? Response;
    public required string Path;
    public required string RequestJson;
    public required string ResponseJson;
    public Exception? Exception;
}