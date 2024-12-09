using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator(IHttpContentResolver httpContentResolver, 
        IEnumerable<IValidator<UpdateOrderItemRequest>> validators,
        IOrderItemsRepository orderItemsRepository)
    {
        RuleFor(x => x.OrderId)
        .Cascade(CascadeMode.Stop)
        .MustBeValidGuid();

        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, cancellationToken) => {
                var orderItem = await orderItemsRepository.GetAsync(Guid.Parse(id));
                if (orderItem == null)
                {
                    context.AddFailure("Order item not found.");
                } else if(orderItem.OrderId == Guid.Parse(context.InstanceToValidate.OrderId))
                {
                    context.AddFailure("Order item does not belong to the order.");
                }
            });

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}