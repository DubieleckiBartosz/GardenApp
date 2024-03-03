namespace BuildingBlocks.Application.Validators;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, string> StringValidator<T>(this IRuleBuilder<T, string> ruleBuilder, int min = 1, int max = 50)
    {
        return ruleBuilder.NotEmpty()
            .NotNull()
            .Length(min, max)
            .WithMessage("{PropertyName}" + $"must be between {min} and {max} characters.");
    }
}