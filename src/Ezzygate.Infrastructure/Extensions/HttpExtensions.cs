using System.Web;

namespace Ezzygate.Infrastructure.Extensions;

public static class HttpExtensions
{
    public static string ToEncodedUrl(this string source, System.Text.Encoding? encoding = null)
    {
        return encoding == null ? HttpUtility.UrlEncode(source) : HttpUtility.UrlEncode(source, encoding);
    }

    public static string ToDecodedUrl(this string source, System.Text.Encoding? encoding = null)
    {
        return encoding == null ? HttpUtility.UrlDecode(source) : HttpUtility.UrlDecode(source, encoding);
    }
}