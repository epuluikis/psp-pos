using FluentValidation;
using Looms.PoS.Application.Models.Requests.Business;

namespace Looms.PoS.Application.Features.Business.Commands.UpdateBusiness;

public class UpdateBusinessRequestValidator : AbstractValidator<UpdateBusinessRequest>
{
    public UpdateBusinessRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
