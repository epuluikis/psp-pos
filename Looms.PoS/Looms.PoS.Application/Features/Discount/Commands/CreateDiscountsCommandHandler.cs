using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands;
public class CreateDiscountsCommandHandler : IRequestHandler<CreateDiscountsCommand, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IHttpContentResolver _httpContentResolver;

    public CreateDiscountsCommandHandler(IDiscountsRepository discountsRepository, IHttpContentResolver httpContentResolver)
    {
        _discountsRepository = discountsRepository;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(CreateDiscountsCommand command, CancellationToken cancellationToken)
    {
        var discountRequest = await _httpContentResolver.GetPayloadAsync<CreateDiscountRequest>(command.Request);
        var discountDao = await _discountsRepository.CreateAsync(new DiscountDao());
        return new CreatedAtRouteResult("test", discountDao);
    }
}
