namespace Offers.Application.Handlers;

public class CompleteOfferHandler : ICommandHandler<CompleteOfferCommand, Response>
{
    public record CompleteOfferCommand(int GardenOfferId) : ICommand<Response>;

    private readonly IGardenOfferRepository _gardenOfferRepository;

    public CompleteOfferHandler(IGardenOfferRepository gardenOfferRepository)
    {
        _gardenOfferRepository = gardenOfferRepository;
    }

    public async Task<Response> Handle(CompleteOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _gardenOfferRepository.GetGardenOfferWithItemsByIdAsync(request.GardenOfferId);

        if (offer == null)
        {
            throw new NotFoundException(ErrorMessages.OfferNotFound(request.GardenOfferId));
        }

        offer.Complete();

        await _gardenOfferRepository.UnitOfWork.SaveAsync(cancellationToken);

        return Response.Ok();
    }
}