namespace BuildingBlocks.Application.Wrappers;

public class ResponseClient
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}

public class ResponseClient<T> : ResponseClient
{
    public T? Data { get; set; }
}