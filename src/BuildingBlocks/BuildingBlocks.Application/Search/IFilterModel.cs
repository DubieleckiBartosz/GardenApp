using BuildingBlocks.Application.Search.SearchParameters;

namespace BuildingBlocks.Application.Search;

public interface IFilterModel
{
    SortModelParameters Sort { get; init; }
}