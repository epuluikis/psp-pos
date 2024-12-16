using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, IActionResult>
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IReservationModelsResolver _modelsResolver;
    private readonly INotificationService _notificationService;
    private readonly ILogger<CreateReservationCommandHandler> _logger;

    public CreateReservationCommandHandler(
        IReservationsRepository reservationsRepository,
        IHttpContentResolver httpContentResolver,
        IReservationModelsResolver modelsResolver,
        INotificationService notificationService,
        ILogger<CreateReservationCommandHandler> logger
    )
    {
        _reservationsRepository = reservationsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task<IActionResult> Handle(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        var reservationRequest = await _httpContentResolver.GetPayloadAsync<CreateReservationRequest>(command.Request);

        var reservationDao = _modelsResolver.GetDaoFromRequest(reservationRequest);

        reservationDao = await _reservationsRepository.CreateAsync(reservationDao);

        try
        {
            await _notificationService.SendReservationNotification(reservationDao);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "unable to send reservation notification");
        }

        var response = _modelsResolver.GetResponseFromDao(reservationDao);

        return new CreatedAtRouteResult($"/reservations/{reservationDao.Id}", response);
    }
}
