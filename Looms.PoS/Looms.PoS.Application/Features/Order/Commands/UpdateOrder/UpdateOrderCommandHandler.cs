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
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public UpdateOrderCommandHandler(
        IOrdersRepository ordersRepository,
        IHttpContentResolver httpContentResolver,
        IOrderModelsResolver orderModelsResolver
    )
    {
        _ordersRepository = ordersRepository;
        _httpContentResolver = httpContentResolver;
        _orderModelsResolver = orderModelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var updateOrderRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderRequest>(command.Request);

        var originalDao = await _ordersRepository.GetAsync(Guid.Parse(command.Id));

        var giftCardDao = _orderModelsResolver.GetDaoFromDaoAndRequest(originalDao, updateOrderRequest);
        giftCardDao = await _ordersRepository.UpdateAsync(giftCardDao);

        var response = _orderModelsResolver.GetResponseFromDao(giftCardDao);

        return new OkObjectResult(response);
    }
}
