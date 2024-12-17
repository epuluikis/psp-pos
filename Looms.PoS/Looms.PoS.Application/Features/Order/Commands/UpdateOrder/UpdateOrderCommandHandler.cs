using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _modelsResolver;

    public UpdateOrderCommandHandler(
        IOrdersRepository ordersRepository,
        IDiscountsRepository discountsRepository,
        IHttpContentResolver httpContentResolver,
        IOrderModelsResolver modelsResolver)
    {
        _ordersRepository = ordersRepository;
        _discountsRepository = discountsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderRequest>(command.Request);

        var original = await _ordersRepository.GetAsync(Guid.Parse(command.Id));
        var discountDao = orderRequest.DiscountId is not null
            ? await _discountsRepository.GetAsync(Guid.Parse(orderRequest.DiscountId))
            : null;

        var orderDao = _modelsResolver.GetDaoFromDaoAndRequest(original, orderRequest, discountDao);
        var updatedOrderDao = await _ordersRepository.UpdateAsync(orderDao);

        var response = _modelsResolver.GetResponseFromDao(updatedOrderDao);

        return new OkObjectResult(response);
    }
}
