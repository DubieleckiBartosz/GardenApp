namespace Offers.Application.Validators;

public class AddGardenOfferItemCommandValidator : AbstractValidatorNotNull<AddGardenOfferItemCommand>
{
    public AddGardenOfferItemCommandValidator()
    {
        RuleFor(_ => _.Name).NotEmpty().NotNull();
        RuleFor(_ => _.Price).GreaterThan(0);
    }
}