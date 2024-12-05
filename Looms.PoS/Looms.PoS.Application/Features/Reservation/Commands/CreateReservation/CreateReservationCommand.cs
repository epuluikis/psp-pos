using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public record CreateReservationCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateReservationCommand(HttpRequest request) : base(request)
    {
    }
}
