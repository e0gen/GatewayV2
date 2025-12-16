namespace Ezzygate.Infrastructure.Cryptography.Exceptions;

public class KeyCheckException : Exception
{
    public string CheckCode { get; }
    public KeyCheckException(string checkCode, string message) : base(message)
    {
        CheckCode = checkCode;
    }
}