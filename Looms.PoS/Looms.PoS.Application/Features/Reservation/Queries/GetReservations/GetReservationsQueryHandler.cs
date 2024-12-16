using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Queries.GetReservations;

public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationModelsResolver _modelsResolver;

    public GetReservationsQueryHandler(IReservationsRepository reservationsRepository, IReservationModelsResolver modelsResolver)
    {
        _reservationsRepository = reservationsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
    {
        var ReservationDaos = await _reservationsRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(ReservationDaos);

        return new OkObjectResult(response);
    }
}
