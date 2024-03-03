namespace BuildingBlocks.Application.Search;

public class SortModel
{
    public string Type { get; }
    public string Name { get; }

    public SortModel(string type, string name)
    {
        Type = type;
        Name = name;
    }
}