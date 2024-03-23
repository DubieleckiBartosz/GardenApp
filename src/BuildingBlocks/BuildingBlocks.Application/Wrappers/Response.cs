namespace BuildingBlocks.Application.Wrappers;

public class Response
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }
    public IEnumerable<string>? Errors { get; set; }

    /// <summary>
    /// Empty constructor for integration tests
    /// </summary>
    public Response()
    { }

    protected Response(string? message, bool success, IEnumerable<string>? errors)
    {
        Message = message;
        Success = success;
        Errors = errors;
    }

    public static Response Ok()
    {
        return new Response(null, true, null);
    }

    public static Response Ok(string? message)
    {
        return new Response(message, true, null);
    }

    public static Response Error(string message)
    {
        return new Response(message, false, null);
    }

    public static Response Error(IEnumerable<string> errors)
    {
        return new Response(null, false, errors);
    }
}

public class Response<T> : Response
{
    public T? Data { get; private set; }

    /// <summary>
    /// Empty constructor for integration tests
    /// </summary>
    public Response()
    { }

    private Response(
        T? data,
        bool success,
        string? message,
        IEnumerable<string>? errors) : base(message, success, errors)
    {
        Data = data;
    }

    public static Response<T> Ok(T data, string? message = null)
    {
        return new Response<T>(data, true, message, null);
    }

    public static Response<T> ErrorResult(string error)
    {
        return new Response<T>(default, false, null, new List<string>() { error });
    }

    public static Response<T> ErrorResult(IEnumerable<string> errors)
    {
        return new Response<T>(default, false, null, errors);
    }
}