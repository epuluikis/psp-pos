using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Reservation.Queries.GetReservations;

public record GetReservationsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetReservationsQuery(HttpRequest request) : base(request)
    {
    }
}
