using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _modelsResolver;

    public GetOrdersQueryHandler(IOrdersRepository ordersRepository, IOrderModelsResolver modelsResolver)
    {
        _ordersRepository = ordersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var filter = _modelsResolver.GetFiltersFromQuery(request);

        var orderDaos = await _ordersRepository.GetAllAsync(filter);

        var response = _modelsResolver.GetResponseFromDao(orderDaos);

        return new OkObjectResult(response);
    }
}
