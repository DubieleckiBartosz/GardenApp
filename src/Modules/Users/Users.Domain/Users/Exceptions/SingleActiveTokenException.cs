namespace Users.Domain.Users.Exceptions;

internal class SingleActiveTokenException : BaseException
{
    public SingleActiveTokenException() : base("New refresh token cannot be created while another is active.")
    {
    }
}