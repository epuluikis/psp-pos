using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceRequestValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceRequestValidator(
        ITaxesRepository taxesRepository)
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

        RuleFor(x => x.TaxId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (taxId, context, _) =>
            {
                var tax = await taxesRepository.GetByTaxCategoryAndBusinessIdAsync(
                    Enum.Parse<TaxCategory>(context.InstanceToValidate.Category),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                );

                if(tax.TaxCategory is not TaxCategory.Service){
                    context.AddFailure("Provided tax should be for service.");
                }
            });
    }
}
