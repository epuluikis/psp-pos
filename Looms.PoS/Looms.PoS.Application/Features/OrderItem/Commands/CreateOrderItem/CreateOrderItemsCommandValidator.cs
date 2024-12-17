using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Commands.CreateOrderItem;

public class CreateOrderItemsCommandValidator : AbstractValidator<CreateOrderItemsCommand>
{
    public CreateOrderItemsCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<CreateOrderItemRequest>> validators,
        IOrdersRepository ordersRepository
    )
    {
        RuleFor(x => x.OrderId)
            .MustBeValidGuid()
            .CustomAsync(async (productId, context, _) =>
                {
                    var orderDao = await ordersRepository.GetAsyncByIdAndBusinessId(
                        Guid.Parse(productId!),
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    );

                    if (orderDao.Status != OrderStatus.Pending)
                    {
                        context.AddFailure($"Cannot update order with status {orderDao.Status}.");
                    }
                }
            );

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
                {
                    var body = await httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(request);
                    var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                    await Task.WhenAll(validationResults);
                }
            );
    }
}
