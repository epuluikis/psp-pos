using FluentValidation;
using Looms.PoS.Application.Models.Requests;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Owner)
            .NotEmpty();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
