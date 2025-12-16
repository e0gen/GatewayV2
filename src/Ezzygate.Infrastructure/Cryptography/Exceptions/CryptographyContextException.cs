namespace Ezzygate.Infrastructure.Cryptography.Exceptions;

public class CryptographyContextException : Exception
{
    public CryptographyContextException() 
        : base($"{nameof(CryptographyContext)} has not been initialized.") { }
}