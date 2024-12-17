using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Order;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrdersCommand>
{
    public CreateOrderCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateOrderRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateOrderRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
