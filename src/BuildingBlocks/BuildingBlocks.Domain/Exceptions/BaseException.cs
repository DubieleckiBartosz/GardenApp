namespace BuildingBlocks.Domain.Exceptions;

public class BaseException : Exception
{
    public string? Title { get; }
    public HttpStatusCode? StatusCode { get; }

    public BaseException(string? title, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) :
        base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }

    public BaseException(string? title, string message, HttpStatusCode? statusCode = null) :
        base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }

    public BaseException(string message, HttpStatusCode statusCode) :
        this(null, message, statusCode)
    {
        StatusCode = statusCode;
    }

    public BaseException(string title, string message) :
        this(title, message, null)
    {
    }

    public BaseException(string message) :
        this(null, message, null)
    {
    }
}