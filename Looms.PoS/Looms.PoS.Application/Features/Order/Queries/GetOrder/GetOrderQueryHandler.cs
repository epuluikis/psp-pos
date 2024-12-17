using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrder;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public GetOrderQueryHandler(IOrdersRepository ordersRepository, IOrderModelsResolver orderModelsResolver)
    {
        _ordersRepository = ordersRepository;
        _orderModelsResolver = orderModelsResolver;
    }

    public async Task<IActionResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var orderDao = await _ordersRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response);
    }
}
