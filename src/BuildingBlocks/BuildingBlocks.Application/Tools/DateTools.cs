namespace BuildingBlocks.Application.Tools;

public static class DateTools
{
    public static DateTime? ToUTC(this DateTime? date) => date.HasValue ? date.Value.ToUniversalTime() : null;

    public static DateTime ToUTC(this DateTime date) => date.ToUniversalTime();
}