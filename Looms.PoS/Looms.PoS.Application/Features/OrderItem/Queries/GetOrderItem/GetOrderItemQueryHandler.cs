using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Queries;

public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;

    public GetOrderItemQueryHandler(IOrderItemsRepository orderItemsRepository, IOrderItemModelsResolver modelsResolver)
    {
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
    }
    
    public async Task<IActionResult> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
    {
        var orderItemDao = await _orderItemsRepository.GetAsync(Guid.Parse(request.Id));
        var response = _modelsResolver.GetResponseFromDao(orderItemDao);
        return new OkObjectResult(response);
    }
}
