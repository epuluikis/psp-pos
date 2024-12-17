using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Queries.GetOrderItem;

public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;

    public GetOrderItemQueryHandler(IOrderItemsRepository orderItemsRepository, IOrderItemModelsResolver modelsResolver)
    {
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetOrderItemQuery query, CancellationToken cancellationToken)
    {
        var orderItemDao = await _orderItemsRepository.GetAsyncByIdAndOrderIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(query.OrderId),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(orderItemDao);

        return new OkObjectResult(response);
    }
}
