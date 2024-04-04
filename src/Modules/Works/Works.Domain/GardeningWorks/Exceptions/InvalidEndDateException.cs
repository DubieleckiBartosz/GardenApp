namespace Works.Domain.GardeningWorks.Exceptions;

internal class InvalidEndDateException : BaseException
{
    internal InvalidEndDateException(int gardeningWorkId)
        : base($"EndDate must be younger than the start date. [GardeningWorkId: {gardeningWorkId}].")
    {
    }
}