namespace Offers.Domain.Exceptions;

internal class IncorrectStatusException : BaseException
{
    public IncorrectStatusException(string currentStatus, string status) : base($"Cannot change status to \"{status}\". [Current: {currentStatus}]")
    {
    }
}