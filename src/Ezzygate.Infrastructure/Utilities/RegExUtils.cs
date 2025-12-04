using System.Text.RegularExpressions;

namespace Ezzygate.Infrastructure.Utilities;

public static partial class RegExUtils
{
    [GeneratedRegex(@"^[a-zA-Z]+$")]
    public static partial Regex LettersOnly();
}