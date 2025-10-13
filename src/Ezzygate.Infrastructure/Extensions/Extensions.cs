namespace Ezzygate.Infrastructure.Extensions;

public static class Extensions
{
    public static T NotNull<T>(this T? value, string? paramName = null) where T : class
    {
        return value ?? throw new ArgumentNullException(paramName);
    }

    public static T NotNull<T>(this T? value, string? paramName = null) where T : struct
    {
        return value ?? throw new ArgumentNullException(paramName);
    }

    public static string NotNullOrEmpty(this string? value, string? paramName = null)
    {
        if (value == null)
            throw new ArgumentNullException(paramName);

        if (value.Length == 0)
            throw new ArgumentException("Value cannot be empty.", paramName);

        return value;
    }

    public static string? NullIfEmpty(this string? source)
    {
        if (source == null || source.Trim().Length == 0)
            return null;

        return source;
    }

    public static string EmptyIfNull(this string? source)
    {
        if (source == null)
            return "";

        return source;
    }

    public static string ValueIfNull(this string? source, string value)
    {
        if (source == null)
            return value;

        return source;
    }

    public static T? NullIfValue<T>(this T? source, T value) where T : struct
    {
        return value.Equals(source) ? null : source;
    }

    public static string Truncate(this string? input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input.Length <= maxLength ? input : input[..maxLength];
    }
}