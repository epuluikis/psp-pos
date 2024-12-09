using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Looms.PoS.Application.Features.OrderItem.Commands.CreateOrderItem;

public record CreateOrderItemsCommandHandler : IRequestHandler<CreateOrderItemsCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IOrderItemModelsResolver _modelsResolver;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrdersRepository _ordersRepository;

    public CreateOrderItemsCommandHandler(IHttpContentResolver httpContentResolver,
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver modelsResolver,
        IOrderModelsResolver orderModelsResolver,
        IOrdersRepository ordersRepository)
    {
        _httpContentResolver = httpContentResolver;
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
        _orderModelsResolver = orderModelsResolver;
        _ordersRepository = ordersRepository;
    }

    public async Task<IActionResult> Handle(CreateOrderItemsCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(command.Request);
        var orderItemDao = _modelsResolver.GetDaoFromRequest(orderItemRequest);

        var createdOrderItem = await _orderItemsRepository.CreateAsync(orderItemDao);
        var orderDao = await _ordersRepository.GetAsync(createdOrderItem.OrderId);
        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response); 
    }
}