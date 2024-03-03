namespace BuildingBlocks.Domain.Exceptions;

internal class InvalidEmailException : BaseException
{
    public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
    {
    }
}