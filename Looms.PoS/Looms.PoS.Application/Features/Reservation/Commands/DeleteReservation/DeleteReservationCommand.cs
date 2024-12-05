using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Commands.DeleteReservation;

public record DeleteReservationCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteReservationCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}