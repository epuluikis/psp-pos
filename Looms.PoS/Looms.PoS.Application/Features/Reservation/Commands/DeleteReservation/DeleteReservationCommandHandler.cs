using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Commands.DeleteReservation;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationModelsResolver _modelsResolver;

    public DeleteReservationCommandHandler(
        IReservationsRepository reservationsRepository,
        IReservationModelsResolver modelsResolver)
    {
        _reservationsRepository = reservationsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteReservationCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _reservationsRepository.GetAsync(Guid.Parse(command.Id));

        var reservationDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _reservationsRepository.UpdateAsync(reservationDao);

        return new NoContentResult();
    }
}