using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateOrderRequest>> validators,
        IOrdersRepository ordersRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _) =>
            {
                var orderDao = await ordersRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                );

                if (orderDao.Status != OrderStatus.Pending)
                {
                    context.AddFailure($"Cannot update order with status {orderDao.Status}.");
                }
            });

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateOrderRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
