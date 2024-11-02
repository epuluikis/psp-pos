using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;

    public GetBusinessesQueryHandler(IBusinessesRepository businessesRepository)
    {
        _businessesRepository = businessesRepository;
    }

    public async Task<IActionResult> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);

        var businessDaos = _businessesRepository.GetAll();

        return new OkObjectResult(businessDaos);
    }
}
