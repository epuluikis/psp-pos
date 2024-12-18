using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IDiscountModelsResolver _modelsResolver;

    public UpdateDiscountCommandHandler(
        IDiscountsRepository discountsRepository,
        IHttpContentResolver httpContentResolver,
        IDiscountModelsResolver modelsResolver)
    {
        _discountsRepository = discountsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateDiscountCommand command, CancellationToken cancellationToken)
    {
        var discountRequest = await _httpContentResolver.GetPayloadAsync<UpdateDiscountRequest>(command.Request);

        var original = await _discountsRepository.GetAsync(Guid.Parse(command.Id));

        var discountDao = _modelsResolver.GetDaoFromDaoAndRequest(original, discountRequest);
        var updateDiscountDao = await _discountsRepository.UpdateAsync(discountDao);

        var response = _modelsResolver.GetResponseFromDao(updateDiscountDao);

        return new OkObjectResult(response);
    }
}
