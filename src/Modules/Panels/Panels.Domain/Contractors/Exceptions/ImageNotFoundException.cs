namespace Panels.Domain.Contractors.Exceptions;

internal class ImageNotFoundException : BaseException
{
    internal ImageNotFoundException(string key) : base($"Image not found. [key: {key}]")
    {
    }
}