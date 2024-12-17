using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateDiscountRequest>> validators,
        IDiscountsRepository discountsRepository
    )
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (productId, context, _)
                => await discountsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(productId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                )
            );

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateDiscountRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
