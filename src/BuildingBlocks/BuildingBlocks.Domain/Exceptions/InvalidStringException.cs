namespace BuildingBlocks.Domain.Exceptions;

public class InvalidStringException : BaseException
{
    public InvalidStringException(string value) : base($"Value: '{value}' is invalid.")
    {
    }
}