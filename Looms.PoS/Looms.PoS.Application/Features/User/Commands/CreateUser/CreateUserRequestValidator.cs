using FluentValidation;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.User.Commands.CreateUser;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IUsersRepository usersRepository, IBusinessesRepository businessesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (email, cancellationToken) => !await usersRepository.ExistsWithEmail(email))
            .WithMessage("User with this email already exists");

        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => Enum.TryParse<UserRole>(x, out _))
            .WithMessage("Invalid role");

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
