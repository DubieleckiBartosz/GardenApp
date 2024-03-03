using BuildingBlocks.Domain.ValueObjects;

namespace BuildingBlocks.Domain.Time;

public class Clock
{
    public static DateTime CurrentDate() => DateTime.UtcNow;

    public static Date CurrentDateObject() => new Date(CurrentDate());
}