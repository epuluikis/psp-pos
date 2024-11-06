using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands;
public record CreateDiscountsCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateDiscountsCommand(HttpRequest request) : base(request)
    {
    }
}
