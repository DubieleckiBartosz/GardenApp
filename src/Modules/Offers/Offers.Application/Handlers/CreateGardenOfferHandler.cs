namespace Offers.Application.Handlers;

public class CreateGardenOfferHandler : ICommandHandler<CreateGardenOfferCommand, Response<CreateGardenOfferResponse>>
{
    public record CreateGardenOfferResponse(int NewOfferId);
    public record CreateGardenOfferCommand(
        string Recipient,
        string Description,
        decimal Price,
        DateTime? ExpirationDate = null) : ICommand<Response<CreateGardenOfferResponse>>;

    private readonly IGardenOfferRepository _gardenOfferRepository;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger _logger;

    public CreateGardenOfferHandler(IGardenOfferRepository gardenOfferItemRepository, ICurrentUser currentUser, ILogger logger)
    {
        _gardenOfferRepository = gardenOfferItemRepository;
        _currentUser = currentUser;
        _logger = logger;
    }

    public async Task<Response<CreateGardenOfferResponse>> Handle(CreateGardenOfferCommand request, CancellationToken cancellationToken)
    {
        var currentOffer = await _gardenOfferRepository.GetGardenOfferByRecipientAndStatusNTAsync(request.Recipient, OfferStatus.Pending);
        if (currentOffer != null)
        {
            throw new BadRequestException(ErrorMessages.SingleOfferInPendingStatus);
        }

        var newOffer = GardenOffer.NewGardenOffer(
            _currentUser.UserId,
            _currentUser.UserName,
            request.Recipient,
            request.Description,
            request.Price,
            request.ExpirationDate);

        await _gardenOfferRepository.AddAsync(newOffer);
        await _gardenOfferRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.Information($"A new gardening offer has been created [CreatorId: {_currentUser.UserId}, Recipient: {request.Recipient}]");

        return Response<CreateGardenOfferResponse>.Ok(new(newOffer.Id));
    }
}