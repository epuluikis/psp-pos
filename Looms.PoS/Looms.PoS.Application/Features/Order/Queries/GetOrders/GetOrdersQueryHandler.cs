using Looms.PoS.Application.Features.Order.Queries.GetOrders;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries;

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
        var orderDaos = await _ordersRepository.GetAllAsync();

        if(request.Status != null)
        {
            var status = Enum.Parse<OrderStatus>(request.Status);
            orderDaos = orderDaos.Where(x => x.Status == status).ToList();
        }
        if(request.UserId != null)
        {
            var userId = Guid.Parse(request.UserId);
            orderDaos = orderDaos.Where(x => x.UserId == userId).ToList();
        }

        var response = _modelsResolver.GetResponseFromDao(orderDaos);

        return new OkObjectResult(response);
    }
}