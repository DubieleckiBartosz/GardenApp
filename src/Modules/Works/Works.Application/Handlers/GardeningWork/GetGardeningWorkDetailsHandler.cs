namespace Works.Application.Handlers.GardeningWork;

internal class GetGardeningWorkDetailsHandler : IQueryHandler<GetGardeningWorkDetailsQuery, Response<GetGardeningWorkDetailsResponse>>
{
    public record GetGardeningWorkDetailsQuery(int GardeningWorkId) : IQuery<Response<GetGardeningWorkDetailsResponse>>;

    public class GetGardeningWorkDetailsResponse
    {
    }

    private readonly IGardeningWorkRepositoryDao _gardeningWorkRepositoryDao;
    private readonly ICurrentUser _currentUser;

    public GetGardeningWorkDetailsHandler(IGardeningWorkRepositoryDao gardeningWorkRepositoryDao, ICurrentUser currentUser)
    {
        _gardeningWorkRepositoryDao = gardeningWorkRepositoryDao;
        _currentUser = currentUser;
    }

    public async Task<Response<GetGardeningWorkDetailsResponse>> Handle(GetGardeningWorkDetailsQuery request, CancellationToken cancellationToken)
    {
        var gardeningWork = await _gardeningWorkRepositoryDao.GetGardeningWorkDetails(request.GardeningWorkId);
        if (gardeningWork == null || gardeningWork.BusinessId != _currentUser.UserId)
        {
            throw new NotFoundException(AppError.GardeningWorkNotFound(request.GardeningWorkId));
        }

        return null;
    }
}