using System.Net;

namespace Ezzygate.WebApi.Extensions;

public static class HttpConnectionExtensions
{
    public static bool IsLocal(this ConnectionInfo connection)
    {
        if (connection.RemoteIpAddress == null)
            return true;

        if (connection.LocalIpAddress != null)
        {
            return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
        }

        return IPAddress.IsLoopback(connection.RemoteIpAddress);
    }
} 