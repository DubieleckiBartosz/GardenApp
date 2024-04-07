namespace Works.Domain.WorkItems.Exceptions;

internal class StartDateAfterEndDateException : BaseException
{
    public StartDateAfterEndDateException(DateTime startDate, DateTime endDate)
        : base($"EndDate must be greater than StartDate. [StartDate: {startDate}, EndDate: {endDate}].")
    {
    }
}