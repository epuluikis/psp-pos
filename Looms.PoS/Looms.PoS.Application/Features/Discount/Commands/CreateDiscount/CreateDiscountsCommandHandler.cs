using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountsCommandHandler : IRequestHandler<CreateDiscountsCommand, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IDiscountModelsResolver _discountModelsResolver;

    public CreateDiscountsCommandHandler(
        IDiscountsRepository discountsRepository,
        IHttpContentResolver httpContentResolver,
        IDiscountModelsResolver discountModelsResolver
    )
    {
        _discountsRepository = discountsRepository;
        _httpContentResolver = httpContentResolver;
        _discountModelsResolver = discountModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateDiscountsCommand command, CancellationToken cancellationToken)
    {
        var discountRequest = await _httpContentResolver.GetPayloadAsync<CreateDiscountRequest>(command.Request);

        var discountDao = _discountModelsResolver.GetDaoFromRequestAndBusinessId(
            discountRequest,
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(command.Request))
        );

        discountDao = await _discountsRepository.CreateAsync(discountDao);

        var response = _discountModelsResolver.GetResponseFromDao(discountDao);

        return new CreatedAtRouteResult($"/discounts/{discountDao.Id}", response);
    }
}
