using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;

    public CreateBusinessCommandHandler(IBusinessesRepository businessesRepository, IHttpContentResolver httpContentResolver)
    {
        _businessesRepository = businessesRepository;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
    {
        var businessRequest = await _httpContentResolver.GetPayloadAsync<CreateBusinessRequest>(command.Request);

        var businessDao = await _businessesRepository.CreateAsync(new BusinessDao());

        return new CreatedAtRouteResult("test", businessDao);
    }
}
