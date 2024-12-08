using FluentValidation;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.User.Commands.UpdateUser;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator(IBusinessesRepository businessesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Role)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(x => Enum.TryParse<UserRole>(x, out _))
            .WithMessage("Invalid role");

        RuleFor(x => x.BusinessId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await businessesRepository.GetAsync(Guid.Parse(id)));
    }
}
