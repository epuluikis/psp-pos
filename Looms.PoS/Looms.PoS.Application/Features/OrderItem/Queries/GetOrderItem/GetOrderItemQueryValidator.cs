using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Queries.GetOrderItems;

public class GetOrderItemQueryValidator : AbstractValidator<GetOrderItemQuery>
{
    public GetOrderItemQueryValidator(IOrderItemsRepository orderItemsRepository)
    {
        RuleFor(x => x.OrderId)
        .MustBeValidGuid();
        
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, cancellationToken) =>
            {
                var orderItem = await orderItemsRepository.GetAsync(Guid.Parse(id));
                if (orderItem.OrderId != Guid.Parse(context.InstanceToValidate.OrderId))
                {
                    context.AddFailure("Order item does not belong to the order");
                }
            });
    }
}