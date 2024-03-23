namespace Panels.Domain.Exceptions;

internal class ImageNotFoundException : BaseException
{
    internal ImageNotFoundException(string key) : base($"Image not found. [key: {key}]")
    {
    }
}