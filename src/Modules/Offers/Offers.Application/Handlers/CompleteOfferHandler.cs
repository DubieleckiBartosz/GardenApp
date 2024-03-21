namespace Offers.Application.Handlers;

internal class CompleteOfferHandler : ICommandHandler<CompleteOffer, Response>
{
    public record CompleteOffer(int GardenOfferId) : ICommand<Response>;

    private readonly IGardenOfferRepository _gardenOfferRepository;

    public CompleteOfferHandler(IGardenOfferRepository gardenOfferRepository)
    {
        _gardenOfferRepository = gardenOfferRepository;
    }

    public async Task<Response> Handle(CompleteOffer request, CancellationToken cancellationToken)
    {
        var offer = await _gardenOfferRepository.GetGardenOfferByIdAsync(request.GardenOfferId);

        if (offer == null)
        {
            throw new NotFoundException(ErrorMessages.OfferNotFound(request.GardenOfferId));
        }

        offer.Complete();

        await _gardenOfferRepository.UnitOfWork.SaveAsync(cancellationToken);

        return new();
    }
}