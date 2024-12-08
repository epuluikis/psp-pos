using FluentValidation;
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
            .CustomAsync(async (id, _, cancellationToken) => await taxesRepository.GetAsync(Guid.Parse(id)));

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateTaxCommand>(request);

                var validationResults = validators.Select(x => x.ValidateAsync((IValidationContext)body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
