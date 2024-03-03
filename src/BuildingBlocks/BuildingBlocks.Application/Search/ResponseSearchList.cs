namespace BuildingBlocks.Application.Search;

public class ResponseSearchList<T> where T : class
{
    public List<T> Data { get; }
    public int TotalCount { get; }

    private ResponseSearchList(List<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }

    public static ResponseSearchList<T>? Create(List<T>? data, int totalCount)
    {
        return data == null ? null : new ResponseSearchList<T>(data, totalCount);
    }
}