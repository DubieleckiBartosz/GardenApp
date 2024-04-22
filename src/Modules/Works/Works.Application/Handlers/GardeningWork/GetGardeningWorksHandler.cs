namespace Works.Application.Handlers.GardeningWork;

public class GetGardeningWorksHandler : IQueryHandler<GetGardeningWorksQuery, Response<GetGardeningWorksResponse>>
{
    public record GetGardeningWorksQuery() : IQuery<Response<GetGardeningWorksResponse>>;

    public class GetGardeningWorksResponse
    {
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;

    public GetGardeningWorksHandler(IGardeningWorkRepository gardeningWorkRepository)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
    }

    public Task<Response<GetGardeningWorksResponse>> Handle(GetGardeningWorksQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}