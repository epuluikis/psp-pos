using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IReservationModelsResolver _modelsResolver;

    public CreateReservationCommandHandler(
        IReservationsRepository reservationsRepository,
        IHttpContentResolver httpContentResolver,
        IReservationModelsResolver modelsResolver)
    {
        _reservationsRepository = reservationsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var reservationRequest = await _httpContentResolver.GetPayloadAsync<CreateReservationRequest>(command.Request);

        var reservationDao = _modelsResolver.GetDaoFromRequest(reservationRequest);
        var createdReservationDao = await _reservationsRepository.CreateAsync(reservationDao);

        var response = _modelsResolver.GetResponseFromDao(createdReservationDao);

        return new CreatedAtRouteResult($"/reservations/{reservationDao.Id}", response);
    }
}
