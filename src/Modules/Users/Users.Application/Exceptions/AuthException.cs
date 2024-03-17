using BuildingBlocks.Domain.Exceptions;

namespace Users.Application.Exceptions;

public class AuthException : BaseException
{
    public AuthException(string message) : base(message)
    {
    }
}