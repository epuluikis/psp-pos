using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;
using System.Data;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateOrderItemsCommandValidator : AbstractValidator<CreateOrderItemsCommand>
{
    public CreateOrderItemsCommandValidator(IHttpContentResolver httpContentResolver, 
        IEnumerable<IValidator<CreateOrderItemRequest>> validators,
        IOrdersRepository ordersRepository)
    {
        RuleFor(x => x.OrderId)
            .MustBeValidGuid()
            .CustomAsync(async (orderId, context, cancellationToken) =>
            {
                var order = await ordersRepository.GetAsync(Guid.Parse(orderId));
            });

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
