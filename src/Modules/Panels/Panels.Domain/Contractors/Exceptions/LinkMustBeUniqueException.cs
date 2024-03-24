namespace Panels.Domain.Contractors.Exceptions;

public class LinkMustBeUniqueException : BaseException
{
    public LinkMustBeUniqueException() : base("Social media link must be unique")
    {
    }
}