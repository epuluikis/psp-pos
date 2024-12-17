using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.User;

namespace Looms.PoS.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateUserRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateUserRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                await Task.WhenAll(validationResults);
            });
    }
}
