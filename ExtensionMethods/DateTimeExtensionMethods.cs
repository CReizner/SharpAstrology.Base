using System;

namespace SharpAstrology.ExtensionMethods;

public static class DateTimeExtensionMethods
{
    /// <summary>
    /// Converts a DateTime object to its corresponding Julian date.
    /// </summary>
    /// <param name="date">The DateTime object to convert.</param>
    /// <returns>The Julian date corresponding to the given DateTime object.</returns>
    public static double ToJulianDate(this DateTime date)
    {
        return date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 86400000 + 2440587.5;
    }
        
}