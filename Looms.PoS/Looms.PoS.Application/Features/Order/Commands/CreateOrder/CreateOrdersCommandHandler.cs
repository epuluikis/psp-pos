using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public CreateOrdersCommandHandler(
        IOrdersRepository ordersRepository,
        IHttpContentResolver httpContentResolver,
        IOrderModelsResolver orderModelsResolver
    )
    {
        _ordersRepository = ordersRepository;
        _httpContentResolver = httpContentResolver;
        _orderModelsResolver = orderModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
    {
        var orderRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderRequest>(command.Request);
        var businessId = HttpContextHelper.GetHeaderBusinessId(command.Request);

        var orderDao = _orderModelsResolver.GetDaoFromRequest(
            orderRequest,
            Guid.Parse(HttpContextHelper.GetUserId(command.Request)),
            Guid.Parse(businessId)
        );
        var userDao = _usersRepository.GetByBusinessAsync(Guid.Parse(orderRequest.UserId), Guid.Parse(businessId));

        orderDao = await _ordersRepository.CreateAsync(orderDao);

        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new CreatedAtRouteResult($"/orders/{orderDao.Id}", response);
    }
}
