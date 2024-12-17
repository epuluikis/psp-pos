using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateOrderItemRequest>> validators,
        IOrderItemsRepository orderItemsRepository
    )
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .DependentRules(() =>
            {
                RuleFor(x => x.OrderId)
                    .Cascade(CascadeMode.Stop)
                    .MustBeValidGuid()
                    .CustomAsync(async (orderId, context, _) =>
                        {
                            var orderItemDao = await orderItemsRepository.GetAsyncByIdAndOrderIdAndBusinessId(
                                Guid.Parse(context.InstanceToValidate.Id),
                                Guid.Parse(orderId!),
                                Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                            );

                            if (orderItemDao.Order.Status != OrderStatus.Pending)
                            {
                                context.AddFailure($"Cannot update order with status {orderItemDao.Order.Status}.");
                            }
                        }
                    );
            });

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
                {
                    var body = await httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(request);
                    var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                    await Task.WhenAll(validationResults);
                }
            );
    }
}
