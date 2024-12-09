using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;

    public UpdateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository, 
        IDiscountsRepository discountsRepository,
        IOrderItemModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver)
    {
        _orderItemsRepository = orderItemsRepository;
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(command.Request);
        
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(command.Id));
        var discountDao = orderItemRequest.DiscountId != null ? await _discountsRepository.GetAsync(Guid.Parse(orderItemRequest.DiscountId)) : null;

        var orderItemDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, orderItemRequest, discountDao);
        var updatedOrderDao = await _orderItemsRepository.UpdateAsync(orderItemDao);

        var response = _modelsResolver.GetResponseFromDao(updatedOrderDao);
        return new OkObjectResult(response);
    }
}