namespace BuildingBlocks.Application.Exceptions;

public class ErrorListException : Exception
{
    public List<string> Errors { get; }

    public ErrorListException() : base("One or more errors have occurred.")
    {
        Errors = new List<string>();
    }

    public ErrorListException(string error) : this()
    {
        Errors.Add(error);
    }

    public ErrorListException(IEnumerable<string> failures) : this()
    {
        foreach (var itemFailure in failures)
        {
            Errors.Add(itemFailure);
        }
    }
}