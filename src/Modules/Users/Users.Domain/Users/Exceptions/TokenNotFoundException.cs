namespace Users.Domain.Users.Exceptions;

internal class TokenNotFoundException : BaseException
{
    public TokenNotFoundException(string token) : base($"Token not found {token}.")
    {
    }
}