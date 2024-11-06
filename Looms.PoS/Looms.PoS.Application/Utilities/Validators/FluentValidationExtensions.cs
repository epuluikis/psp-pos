using FluentValidation;

namespace Looms.PoS.Application.Utilities.Validators;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Must(id => Guid.TryParse(id, out _) && id.Length == 36)
                      .WithMessage("{PropertyName} is not a valid Guid");
    }
}
