namespace Works.Domain.GardeningWorks.Exceptions;

internal class DateMustBeFutureException : BaseException
{
    internal DateMustBeFutureException(DateTime dateTime)
        : base($"Date must be future. [Date: {dateTime}].")
    {
    }
}