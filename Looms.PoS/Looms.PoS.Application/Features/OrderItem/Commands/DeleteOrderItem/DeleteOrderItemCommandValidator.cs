using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemCommandValidator(
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
    }
}