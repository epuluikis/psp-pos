using System.Globalization;

namespace Looms.PoS.Application.Utilities;

public static class DateTimeHelper
{
    public static DateTime ConvertToUtc(string dateString)
    {
        return DateTime.ParseExact(
            dateString,
            "yyyy-MM-dd HH:mm:ss",
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
}