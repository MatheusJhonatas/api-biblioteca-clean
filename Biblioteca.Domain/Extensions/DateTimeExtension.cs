namespace Biblioteca.Domain.Extensions;

public static class DateTimeExtension
{
    public static DateTime ToBrasiliaTime(this DateTime dateTime)
    {
        TimeZoneInfo horaBrasilia;
        try
        {
            horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
        }
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), horaBrasilia);
    }
    public static DateTime NowInBrasilia()
    {
        return DateTime.UtcNow.ToBrasiliaTime();
    }
}