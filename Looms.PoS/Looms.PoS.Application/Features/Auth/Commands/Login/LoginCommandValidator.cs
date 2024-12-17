using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Auth;

namespace Looms.PoS.Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<LoginRequest>> validators)
    {
        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<LoginRequest>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
