using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Commands.UpdateReservation;
public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IReservationModelsResolver _modelsResolver;

    public UpdateReservationCommandHandler(
        IReservationsRepository reservationsRepository,
        IHttpContentResolver httpContentResolver,
        IReservationModelsResolver modelsResolver)
    {
        _reservationsRepository = reservationsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateReservationCommand command, CancellationToken cancellationToken)
    {
        var updateReservationRequest = await _httpContentResolver.GetPayloadAsync<UpdateReservationRequest>(command.Request);

        var originalDao = await _reservationsRepository.GetAsync(Guid.Parse(command.Id));

        var reservationDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateReservationRequest);
        var updatedReservationDao = await _reservationsRepository.UpdateAsync(reservationDao);

        var response = _modelsResolver.GetResponseFromDao(updatedReservationDao);

        return new OkObjectResult(response);
    }
}