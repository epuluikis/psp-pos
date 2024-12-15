using System.Globalization;

namespace Looms.PoS.Application.Utilities;

public static class DateTimeHelper
{
    public static DateTime ConvertToUtc(string dateString)
    {
        return DateTime.ParseExact(
            dateString,
            "dd-MM-yyyy HH:mm:ss",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);
    }

    public static string ConvertToLocal(DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime().ToString("dd-MM-yyyy HH:mm:ss");
    }
    public static bool TryConvertToUtc(string dateString, out DateTime utcDateTime)
    {
        return DateTime.TryParseExact(
            dateString,
            "dd-MM-yyyy HH:mm:ss",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal,
            out utcDateTime);
    }
}