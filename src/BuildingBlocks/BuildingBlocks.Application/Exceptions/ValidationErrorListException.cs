namespace BuildingBlocks.Application.Exceptions;

internal class ValidationErrorListException : Exception
{
    public List<string> Errors { get; }

    public ValidationErrorListException() : base("One or more validation failures have occurred.")
    {
        Errors = new List<string>();
    }

    public ValidationErrorListException(IEnumerable<string> failures) : this()
    {
        foreach (var itemFailure in failures)
        {
            Errors.Add(itemFailure);
        }
    }
}