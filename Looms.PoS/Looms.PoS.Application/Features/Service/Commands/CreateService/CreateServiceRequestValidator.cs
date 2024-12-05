using FluentValidation;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Domain.Exceptions;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    private readonly IBusinessesRepository _businessesRepository;
    public CreateServiceRequestValidator(IBusinessesRepository businessesRepository)
    {
        _businessesRepository = businessesRepository;

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
            .MustBeValidGuid()
            .MustAsync(async (businessId, cancellation) => 
            {
                return Guid.TryParse(businessId, out var guid) && await _businessesRepository.GetAsync(guid) != null;
            })
            .WithMessage("Business does not exist.");
    }
}
