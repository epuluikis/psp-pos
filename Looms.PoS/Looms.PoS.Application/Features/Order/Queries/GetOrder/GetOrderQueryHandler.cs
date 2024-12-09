using Looms.PoS.Application.Features.Order.Queries.GetOrder;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _modelsResolver;

    public GetOrderQueryHandler(IOrdersRepository ordersRepository, IOrderModelsResolver modelsResolver)
    {
        _ordersRepository = ordersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        var orderDao = await _ordersRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response);
    }
}