using FluentValidation;
using Looms.PoS.Application.Models.Requests;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Category)
            .NotEmpty();
            
        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.DurationMin)
            .GreaterThanOrEqualTo(0);

            
    }
}
