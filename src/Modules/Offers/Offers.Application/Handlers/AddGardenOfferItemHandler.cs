namespace Offers.Application.Handlers;

public class AddGardenOfferItemHandler : ICommandHandler<AddGardenOfferItemCommand, Response>
{
    public record AddGardenOfferItemCommand(int OfferId, string Name, string Description, decimal Price) : ICommand<Response>;

    private readonly IGardenOfferRepository _gardenOfferItemRepository;
    private readonly ICurrentUser _currentUser;

    public AddGardenOfferItemHandler(IGardenOfferRepository gardenOfferItemRepository, ICurrentUser currentUser)
    {
        _gardenOfferItemRepository = gardenOfferItemRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddGardenOfferItemCommand request, CancellationToken cancellationToken)
    {
        var offer = await _gardenOfferItemRepository.GetGardenOfferWithItemsByIdAsync(request.OfferId);
        if (offer == null)
        {
            throw new NotFoundException(ErrorMessages.OfferNotFound(request.OfferId));
        }

        var newItem = GardenOfferItem.NewGardenOfferItem(_currentUser.UserId, request.Name, request.Price);

        offer.AddNewOfferItem(newItem);
        await _gardenOfferItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return new();
    }
}