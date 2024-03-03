namespace BuildingBlocks.Application.Exceptions;

internal class BadRequestException : BaseException
{
    public BadRequestException(string? title, string message) :
        base(title, message, HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}