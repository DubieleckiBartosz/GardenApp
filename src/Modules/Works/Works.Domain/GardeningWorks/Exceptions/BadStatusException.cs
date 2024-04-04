namespace Works.Domain.GardeningWorks.Exceptions;

internal class BadStatusException : BaseException
{
    internal BadStatusException(GardeningWorkStatus currentStatus, int gardeningWorkId)
        : base($"In the current status, operation is not impossible. [Current status: {currentStatus}, Identifier: {gardeningWorkId}]")
    {
    }
}