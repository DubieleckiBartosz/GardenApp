namespace BuildingBlocks.Domain.Exceptions;

internal class InvalidPhoneNumberException : BaseException
{
    public InvalidPhoneNumberException(string phoneNumber) : base($"Phone number: '{phoneNumber}' is invalid.")
    {
    }
}