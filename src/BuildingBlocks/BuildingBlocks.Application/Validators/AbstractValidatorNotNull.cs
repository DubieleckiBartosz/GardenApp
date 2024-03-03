using FluentValidation.Results;

namespace BuildingBlocks.Application.Validators;

public class AbstractValidatorNotNull<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        return context.InstanceToValidate == null
            ? new ValidationResult(new[] { new ValidationFailure(nameof(T), "Object cannot be null") })
            : base.Validate(context);
    }
}