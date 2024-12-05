using FluentValidation;

namespace Looms.PoS.Application.Utilities.Validators;

public static class FluentValidationExtensions
{
    private static readonly BusinessHours businessHours = new BusinessHours();
    public static IRuleBuilder<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Must(id => Guid.TryParse(id, out _) && id.Length == 36)
                      .WithMessage("{PropertyName} is not a valid Guid");
    }

    public static IRuleBuilder<T, string> MustBeValidDateTime<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.NotEmpty()
                      .Must(date =>
                      {
                          try
                          {
                              DateTimeHelper.ConvertToUtc(date);
                              return true;
                          }
                          catch (FormatException)
                          {
                              return false;
                          }
                      })
                      .WithMessage("{PropertyName} is not a valid DateTime");
    }
    
    public static IRuleBuilder<T, string> MustBeWithinBusinessHours<T>(this IRuleBuilder<T, string> builder)
    {
        return builder.Must((_, dateString) =>
                      {
                          if (DateTime.TryParse(dateString, out DateTime dateTime))
                          {
                              return dateTime.Hour >= businessHours.StartHour && dateTime.Hour < businessHours.EndHour;
                          }
                          return false;
                      })
                      .WithMessage("{PropertyName} must be between business hours");
    }
}
