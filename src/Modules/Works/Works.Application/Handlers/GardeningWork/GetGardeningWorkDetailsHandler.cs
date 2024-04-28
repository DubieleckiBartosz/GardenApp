namespace Works.Application.Handlers.GardeningWork;

public class GetGardeningWorkDetailsHandler : IQueryHandler<GetGardeningWorkDetailsQuery, Response<GardeningWorkDetailsViewModel>>
{
    public record GetGardeningWorkDetailsQuery(int GardeningWorkId) : IQuery<Response<GardeningWorkDetailsViewModel>>;

    private readonly IGardeningWorkRepositoryDao _gardeningWorkRepositoryDao;
    private readonly ICurrentUser _currentUser;

    public GetGardeningWorkDetailsHandler(IGardeningWorkRepositoryDao gardeningWorkRepositoryDao, ICurrentUser currentUser)
    {
        _gardeningWorkRepositoryDao = gardeningWorkRepositoryDao;
        _currentUser = currentUser;
    }

    public async Task<Response<GardeningWorkDetailsViewModel>> Handle(GetGardeningWorkDetailsQuery request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepositoryDao.GetGardeningWorkDetails(request.GardeningWorkId);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        GardeningWorkDetailsViewModel viewModel = gardeningWork;

        return Response<GardeningWorkDetailsViewModel>.Ok(viewModel);
    }
}