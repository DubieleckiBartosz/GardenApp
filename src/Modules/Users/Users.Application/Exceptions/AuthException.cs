namespace Users.Application.Exceptions;

public class AuthException : BaseException
{
    public AuthException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, statusCode)
    {
    }
}