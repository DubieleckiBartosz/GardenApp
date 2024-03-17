namespace BuildingBlocks.Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string? title, string message) :
        base(title, message, HttpStatusCode.NotFound)
    {
    }
}