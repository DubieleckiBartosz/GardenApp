namespace BuildingBlocks.Domain.Time;

public static class Clock
{
    public static DateTime CurrentDate() => DateTime.UtcNow;

    public static Date CurrentDateObject() => new Date(CurrentDate());
}