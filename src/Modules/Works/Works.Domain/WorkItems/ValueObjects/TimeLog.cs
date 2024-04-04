namespace Works.Domain.WorkItems.ValueObjects;

public class TimeLog : ValueObject
{
    public int Minutes { get; }
    public DateTime Date { get; }
    public DateTime Created { get; }

    private TimeLog()
    { }

    public TimeLog(int minutes, DateTime date) => (Minutes, Date, Created) = (minutes, date, Clock.CurrentDate());

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Minutes;
        yield return Date;
        yield return Created;
    }
}