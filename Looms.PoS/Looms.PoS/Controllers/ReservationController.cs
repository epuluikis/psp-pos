using Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;
using Looms.PoS.Application.Features.Reservation.Commands.DeleteReservation;
using Looms.PoS.Application.Features.Reservation.Commands.UpdateReservation;
using Looms.PoS.Application.Features.Reservation.Queries.GetReservation;
using Looms.PoS.Application.Features.Reservation.Queries.GetReservations;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Models.Responses.Reservation;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerRequestType(typeof(CreateReservationRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Reservation successfully created.", typeof(ReservationResponse))]
    public async Task<IActionResult> CreateReservation()
    {
        var comnand = new CreateReservationCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of reservation returned successfully.", typeof(List<ReservationResponse>))]
    public async Task<IActionResult> GetReservations()
    {
        var query = new GetReservationsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{reservationId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Reservation details returned successfully.", typeof(ReservationResponse))]
    public async Task<IActionResult> GetReservation(string reservationId)
    {
        var query = new GetReservationQuery(GetRequest(), reservationId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{reservationId}}")]
    [SwaggerRequestType(typeof(UpdateReservationRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Reservation successfully updated.", typeof(ReservationResponse))]
    public async Task<IActionResult> UpdateReservation(string reservationId)
    {
        var query = new UpdateReservationCommand(GetRequest(), reservationId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{reservationId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Reservation successfully deleted.")]
    public async Task<IActionResult> DeleteReservation(string reservationId)
    {
        var query = new DeleteReservationCommand(GetRequest(), reservationId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
