namespace Works.Application.Handlers.GardeningWork;

public class GetGardeningWorksHandler : IQueryHandler<GetGardeningWorksQuery, Response<GetGardeningWorksResponse>>
{
    public record GetGardeningWorksQuery() : IQuery<Response<GetGardeningWorksResponse>>;

    public class GetGardeningWorksResponse
    {
        public IEnumerable<GardeningWorkViewModel> GardeningWorks { get; }

        public GetGardeningWorksResponse(IEnumerable<GardeningWorkViewModel> gardeningWorks) => GardeningWorks = gardeningWorks;
    }

    private readonly IGardeningWorkRepositoryDao _gardeningWorkRepository;
    private readonly ICurrentUser _currentUser;

    public GetGardeningWorksHandler(IGardeningWorkRepositoryDao gardeningWorkRepository, ICurrentUser currentUser)
    {
        _gardeningWorkRepository = gardeningWorkRepository;
        _currentUser = currentUser;
    }

    public async Task<Response<GetGardeningWorksResponse>> Handle(GetGardeningWorksQuery request, CancellationToken cancellationToken)
    {
        var gardeningWorks = await _gardeningWorkRepository.GetGardeningWorks(_currentUser.UserId);
        if (gardeningWorks == null)
        {
            throw new NotFoundException(AppError.GardeningWorksNotFound());
        }

        var response = gardeningWorks.Select(_ =>
        {
            GardeningWorkViewModel gardeningWork = _;
            return gardeningWork;
        });

        return Response<GetGardeningWorksResponse>.Ok(new(response));
    }
}