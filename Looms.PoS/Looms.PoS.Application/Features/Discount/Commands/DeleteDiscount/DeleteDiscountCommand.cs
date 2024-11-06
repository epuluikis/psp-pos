using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands;
public record DeleteDiscountCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public DeleteDiscountCommand(HttpRequest request) : base(request)
    {
    }
}
