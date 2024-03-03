namespace BuildingBlocks.Application.Search.SearchParameters;

public class SortModelParameters
{
    public string? Type { get; init; }
    public string? Name { get; init; }

    public SortModelParameters()
    {
    }

    [JsonConstructor]
    public SortModelParameters(string? type, string? name)
    {
        Type = type;
        Name = name;
    }
}