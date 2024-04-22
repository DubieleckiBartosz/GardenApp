namespace Works.Application.Handlers.GardeningWork;

public class GetGardeningWorkByIdHandler : IQueryHandler<GetGardeningWorkByIdQuery, Response<GetGardeningWorkByIdResponse>>
{
    public record GetGardeningWorkByIdQuery() : IQuery<Response<GetGardeningWorkByIdResponse>>;

    public class GetGardeningWorkByIdResponse
    {
    }

    private readonly IGardeningWorkRepository _gardeningWorkRepository;

    public GetGardeningWorkByIdHandler(IGardeningWorkRepository gardeningWorkRepository)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
    }

    public Task<Response<GetGardeningWorkByIdResponse>> Handle(GetGardeningWorkByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}