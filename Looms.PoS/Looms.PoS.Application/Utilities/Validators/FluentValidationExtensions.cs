using FluentValidation;
using Looms.PoS.Application.Utilities.Helpers;

namespace Looms.PoS.Application.Utilities.Validators;

public static class FluentValidationExtensions
{
    public static IRuleBuilder<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Must(id => Guid.TryParse(id, out _) && id.Length == 36)
                      .WithMessage("{PropertyName} is not a valid Guid");
    }

    public static IRuleBuilder<T, string> MustBeValidDateTime<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Must(date => DateTimeHelper.TryConvertToUtc(date, out _))
                      .WithMessage("{PropertyName} is not a valid DateTime");
    }

    public static IRuleBuilder<T, string> MustBeValidPhoneNumber<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{1,4}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$")
                      .WithMessage("{PropertyName} is not a valid phone number");
    }
}
