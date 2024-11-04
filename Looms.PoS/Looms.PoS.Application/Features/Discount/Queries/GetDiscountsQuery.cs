using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries;
public record GetDiscountsQuery : IRequest<IActionResult>
{
}
