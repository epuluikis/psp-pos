using Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;
using Looms.PoS.Application.Features.Reservation.Queries.GetReservation;
using Looms.PoS.Application.Features.Reservation.Queries.GetReservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "reservations";

    public ReservationsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    public async Task<IActionResult> CreateReservation()
    {
        var comnand = new CreateReservationCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetReservations()
    {
        var query = new GetReservationsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{reservationId}}")]
    public async Task<IActionResult> GetReservation(string reservationId)
    {
        var query = new GetReservationQuery(GetRequest(), reservationId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
