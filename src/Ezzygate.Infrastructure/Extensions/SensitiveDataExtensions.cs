using Ezzygate.Application.Interfaces;

namespace Ezzygate.Infrastructure.Extensions;

public static class SensitiveDataExtensions
{
    public static object MaskIfSensitive(this object obj)
    {
        return obj is ISensitiveData sensitiveData 
            ? sensitiveData.Mask() 
            : obj;
    }
}