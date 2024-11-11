using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Queries.GetReservation;

public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationModelsResolver _modelsResolver;

    public GetReservationQueryHandler(IReservationsRepository reservationesRepository, IReservationModelsResolver modelsResolver)
    {
        _reservationsRepository = reservationesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservationDao = await _reservationsRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(reservationDao);

        return new OkObjectResult(response);
    }
}
