using System.Runtime.Versioning;
using System.Security.Cryptography;
using Ezzygate.Infrastructure.Cryptography.Enums;
using Ezzygate.Infrastructure.Cryptography.Interfaces;

namespace Ezzygate.Infrastructure.Win32.Cryptography.Providers;

[SupportedOSPlatform("windows")]
public class WindowsDataProtectionProvider : IDataProtectionProvider
{
    public byte[] Protect(byte[] data, byte[]? optionalEntropy, ProtectScope scope)
    {
        if (scope == ProtectScope.None)
            return data;

        var dataProtectionScope = scope == ProtectScope.CurrentUser
            ? DataProtectionScope.CurrentUser
            : DataProtectionScope.LocalMachine;

        return ProtectedData.Protect(data, optionalEntropy, dataProtectionScope);
    }

    public byte[] Unprotect(byte[] data, byte[]? optionalEntropy, ProtectScope scope)
    {
        if (scope == ProtectScope.None)
            return data;

        var dataProtectionScope = scope == ProtectScope.CurrentUser
            ? DataProtectionScope.CurrentUser
            : DataProtectionScope.LocalMachine;

        return ProtectedData.Unprotect(data, optionalEntropy, dataProtectionScope);
    }
}