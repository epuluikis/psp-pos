using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IOrderItemService _orderItemService;

    public DeleteOrderItemCommandHandler(
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver orderItemModelsResolver,
        IOrderItemService orderItemService
    )
    {
        _orderItemsRepository = orderItemsRepository;
        _orderItemModelsResolver = orderItemModelsResolver;
        _orderItemService = orderItemService;
    }

    public async Task<IActionResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(request.Id));
        var orderItemDao = _orderItemModelsResolver.GetDeletedDao(originalDao);

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _orderItemService.ResetQuantity(originalDao);
            await _orderItemsRepository.UpdateAsync(orderItemDao);

            transactionScope.Complete();
        }

        return new NoContentResult();
    }
}
