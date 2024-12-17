using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Tax.Commands.UpdateTax;

public class UpdateTaxCommandValidator : AbstractValidator<UpdateTaxCommand>
{
    public UpdateTaxCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateTaxRequest>> validators,
        ITaxesRepository taxesRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (taxId, context, _) 
                => await taxesRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(taxId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader]))
            );

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateTaxRequest>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}
