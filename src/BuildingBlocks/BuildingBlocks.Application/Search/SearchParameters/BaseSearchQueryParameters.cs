namespace BuildingBlocks.Application.Search.SearchParameters;

public abstract class BaseSearchQueryParameters
{
    private const int MaxPageSize = 40;
    private const int DefaultPageNumber = 1;
    private int _pageNumber = 1;
    private int _pageSize = 25;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value is <= 0 ? DefaultPageNumber : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value is > MaxPageSize ? MaxPageSize : value is <= 0 ? _pageSize : value;
    }
}