using Ezzygate.Infrastructure.Cryptography.Enums;

namespace Ezzygate.Infrastructure.Cryptography.Interfaces;

public interface IDataProtectionProvider
{
    byte[] Protect(byte[] data, byte[]? optionalEntropy, ProtectScope scope);
    byte[] Unprotect(byte[] data, byte[]? optionalEntropy, ProtectScope scope);
}