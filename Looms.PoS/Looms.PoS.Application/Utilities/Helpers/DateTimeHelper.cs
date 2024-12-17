using System.Globalization;

namespace Looms.PoS.Application.Utilities.Helpers;


public static class DateTimeHelper
{
    public const string DateFormat = "yyyy-MM-dd HH:mm:ss";

    public static DateTime ConvertToUtc(string dateString)
    {
        return DateTime.ParseExact(
            dateString,
            DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal);
    }

    public static bool TryConvertToUtc(string dateString)
    {
        try
        {
            var utcTime = ConvertToUtc(dateString);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string ConvertToLocal(DateTime dateTime)
    {
        return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime().ToString();
    }
    public static bool TryConvertToUtc(string dateString, out DateTime utcDateTime)
    {
        return DateTime.TryParseExact(
            dateString,
            DateFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal | DateTimeStyles.AdjustToUniversal,
            out utcDateTime);
    }
}