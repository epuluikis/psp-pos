using FluentValidation;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.User.Commands.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => Enum.TryParse<UserRole>(x, out _))
            .WithMessage("Invalid role");
    }
}
