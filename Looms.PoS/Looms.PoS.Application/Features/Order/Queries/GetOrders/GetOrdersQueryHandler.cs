using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public GetOrdersQueryHandler(
        IOrdersRepository ordersRepository,
        IOrderModelsResolver orderModelsResolver
    )
    {
        _ordersRepository = ordersRepository;
        _orderModelsResolver = orderModelsResolver;
    }

    public async Task<IActionResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var filter = _orderModelsResolver.GetFiltersFromQuery(query);

        var orderDaos = await _ordersRepository.GetAllAsyncByBusinessId(
            filter,
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _orderModelsResolver.GetResponseFromDao(orderDaos);

        return new OkObjectResult(response);
    }
}
