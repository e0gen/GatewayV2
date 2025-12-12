namespace Ezzygate.Infrastructure.Cryptography.Exceptions;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException() 
        : base("Incorrect password was provided to open the key") { }
}