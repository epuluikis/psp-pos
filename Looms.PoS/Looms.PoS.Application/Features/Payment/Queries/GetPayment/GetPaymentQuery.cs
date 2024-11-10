using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Queries.GetPayment;

public record GetPaymentQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetPaymentQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
