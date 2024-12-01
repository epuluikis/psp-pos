using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;

public record UpdateDiscountCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateDiscountCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
