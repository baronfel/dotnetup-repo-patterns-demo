using System;

namespace MyLib;

public static class StringHelpers
{
    /// <summary>
    /// Truncates a string to the specified maximum length,
    /// appending an ellipsis if the string was shortened.
    /// </summary>
    public static string Truncate(string value, int maxLength)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));
        if (maxLength < 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (value.Length <= maxLength)
            return value;

        if (maxLength <= 3)
            return value.Substring(0, maxLength);

        return value.Substring(0, maxLength - 3) + "...";
    }

    /// <summary>
    /// Returns true if the string contains only ASCII letters and digits.
    /// </summary>
    public static bool IsAlphanumericAscii(string value)
    {
        if (value is null)
            throw new ArgumentNullException(nameof(value));

        foreach (char c in value)
        {
            if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')))
                return false;
        }

        return value.Length > 0;
    }
}
