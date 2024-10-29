using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public record GetBusinessesQuery : IRequest<IActionResult>
{
}
