using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;

    public DeleteOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository, IOrderItemModelsResolver modelsResolver)
    {
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(request.Id));

        var orderItemDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _orderItemsRepository.UpdateAsync(orderItemDao);
        
        return new NoContentResult();
    }
}