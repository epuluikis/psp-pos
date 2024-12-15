using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
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
    private readonly INotificationService _notificationService;

    public CreateReservationCommandHandler(
        IReservationsRepository reservationsRepository,
        IHttpContentResolver httpContentResolver,
        IReservationModelsResolver modelsResolver,
        INotificationService notificationService
    )
    {
        _reservationsRepository = reservationsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _notificationService = notificationService;
    }

    public async Task<IActionResult> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var reservationRequest = await _httpContentResolver.GetPayloadAsync<CreateReservationRequest>(command.Request);

        var reservationDao = _modelsResolver.GetDaoFromRequest(reservationRequest);
        reservationDao = await _reservationsRepository.CreateAsync(reservationDao);

        await _notificationService.SendReservationNotification(reservationDao);

        var response = _modelsResolver.GetResponseFromDao(reservationDao);

        return new CreatedAtRouteResult($"/reservations{reservationDao.Id}", response);
    }
}
