using System.Text.RegularExpressions;

namespace Ezzygate.WebApi.Dtos;

public class Response
{
    public Response(ResultEnum result, object? data = null) : this(result.ToString(), data)
    {
    }

    public Response(string result, object? data = null)
    {
        if (string.IsNullOrEmpty(result))
            throw new ArgumentException("Must provide a result");
        if (!Regex.IsMatch(result, @"^[a-zA-Z]+$"))
            throw new ArgumentException("Result must contain only letters");

        Result = result;
        Data = data;
    }

    public string Result { get; }

    public object? Data { get; }
} 