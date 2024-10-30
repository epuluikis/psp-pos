using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, IActionResult>
{
    public async Task<IActionResult> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(100);

        return new OkObjectResult("hello");
    }
}
