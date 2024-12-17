using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class CreateOrdersCommandHandler : IRequestHandler<CreateOrdersCommand, IActionResult>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IBusinessesRepository _businessRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _modelsResolver;

    public CreateOrdersCommandHandler(
        IOrdersRepository ordersRepository,
        IBusinessesRepository businessRepository,
        IUsersRepository usersRepository,
        IHttpContentResolver httpContentResolver,
        IOrderModelsResolver modelsResolver)
    {
        _ordersRepository = ordersRepository;
        _businessRepository = businessRepository;
        _usersRepository = usersRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateOrdersCommand command, CancellationToken cancellationToken)
    {
        var orderRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderRequest>(command.Request);
        var businessId = HttpContextHelper.GetHeaderBusinessId(command.Request);

        var businessDao = _businessRepository.GetAsync(Guid.Parse(orderRequest.BusinessId));
        var userDao = _usersRepository.GetByBusinessAsync(Guid.Parse(orderRequest.UserId), Guid.Parse(businessId));

        await Task.WhenAll(businessDao, userDao);

        var orderDao = _modelsResolver.GetDaoFromRequest(orderRequest, businessDao.Result, userDao.Result);
        var createdOrderDao = await _ordersRepository.CreateAsync(orderDao);

        var response = _modelsResolver.GetResponseFromDao(createdOrderDao);

        return new CreatedAtRouteResult($"/orders/{orderDao.Id}", response);
    }
}
