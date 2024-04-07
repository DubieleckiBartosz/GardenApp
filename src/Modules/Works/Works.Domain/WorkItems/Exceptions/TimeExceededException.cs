namespace Works.Domain.WorkItems.Exceptions;

internal class TimeExceededException : BaseException
{
    public TimeExceededException(DateTime startDate, DateTime endDate, int maxHours)
        : base($"The difference between StartDate and EndDate must not exceed {maxHours} hours. [StartDate: {startDate}, EndDate: {endDate}].")
    {
    }
}