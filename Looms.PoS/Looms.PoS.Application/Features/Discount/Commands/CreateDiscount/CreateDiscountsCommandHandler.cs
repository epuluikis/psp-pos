using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
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
    private readonly IDiscountModelsResolver _modelsResolver;


    public CreateDiscountsCommandHandler(
        IDiscountsRepository discountsRepository, 
        IHttpContentResolver httpContentResolver,
        IDiscountModelsResolver modelsResolver)
    {
        _discountsRepository = discountsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateDiscountsCommand command, CancellationToken cancellationToken)
    {
        var discountRequest = await _httpContentResolver.GetPayloadAsync<CreateDiscountRequest>(command.Request);
        
        var discountDao = _modelsResolver.GetDaoFromRequest(discountRequest);
        var createdDiscountDao = await _discountsRepository.CreateAsync(discountDao);
        
        var response = _modelsResolver.GetResponseFromDao(createdDiscountDao);

        return new CreatedAtRouteResult($"/discounts/{discountDao.Id}", response);
    }
}
