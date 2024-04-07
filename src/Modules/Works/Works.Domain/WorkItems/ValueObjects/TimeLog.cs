namespace Works.Domain.WorkItems.ValueObjects;

public class TimeLog : ValueObject
{
    private const int maxHours = 24;
    public short Minutes { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public DateTime Created { get; }

    private TimeLog()
    { }

    public TimeLog(DateTime startDate, DateTime endDate)
    {
        if (endDate < startDate)
        {
            throw new StartDateAfterEndDateException(startDate, endDate);
        }

        var value = GetMinutesBetweenDates(startDate, endDate);
        (Minutes, StartDate, EndDate, Created) = (value, startDate, endDate, Clock.CurrentDate());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Minutes;
        yield return StartDate;
        yield return EndDate;
        yield return Created;
    }

    private short GetMinutesBetweenDates(DateTime startDate, DateTime endDate)
    {
        var duration = endDate - startDate;
        if (duration.TotalHours > maxHours)
        {
            throw new TimeExceededException(startDate, endDate, maxHours);
        }

        return (short)Math.Round(duration.TotalMinutes);
    }
}