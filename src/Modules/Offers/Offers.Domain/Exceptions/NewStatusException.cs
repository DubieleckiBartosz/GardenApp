namespace Offers.Domain.Exceptions;

internal class NewStatusException : BaseException
{
    public NewStatusException(string currentStatus, string status) : base($"Cannot change status to '{status}'. [Current: {currentStatus}]")
    {
    }
}