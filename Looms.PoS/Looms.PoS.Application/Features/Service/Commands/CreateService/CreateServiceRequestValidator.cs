using FluentValidation;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator(IBusinessesRepository businessesRepository)
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
            
        RuleFor(x => x.BusinessId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (businessId, context, cancellation) => 
            {
                await businessesRepository.GetAsync(new Guid(businessId));
            });
    }
}
