using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateUserRequest>> validators,
        IUsersRepository usersRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, cancellationToken) =>
                await usersRepository.GetByBusinessAsync(Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])));

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateUserRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                await Task.WhenAll(validationResults);
            });
    }
}