using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Queries.GetRefunds;

public record GetRefundsQuery : IRequest<IActionResult>
{
}
