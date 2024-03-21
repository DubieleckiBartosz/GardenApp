namespace Offers.Application.Handlers;

public class AddGardenOfferItemHandler : ICommandHandler<AddGardenOfferItemCommand, Response>
{
    public record AddGardenOfferItemCommand(string Name, string Description, decimal Price) : ICommand<Response>;

    private readonly IGardenOfferRepository _gardenOfferItemRepository;
    private readonly ICurrentUser _currentUser;

    public AddGardenOfferItemHandler(IGardenOfferRepository gardenOfferItemRepository, ICurrentUser currentUser)
    {
        _gardenOfferItemRepository = gardenOfferItemRepository;
        _currentUser = currentUser;
    }

    public async Task<Response> Handle(AddGardenOfferItemCommand request, CancellationToken cancellationToken)
    {
        var newItem = GardenOfferItem.NewGardenOfferItem(_currentUser.UserId, request.Name, request.Price);

        await _gardenOfferItemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return new();
    }
}