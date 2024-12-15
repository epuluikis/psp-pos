using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProviders;

public record GetPaymentProvidersQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetPaymentProvidersQuery(HttpRequest request) : base(request)
    {
    }
}
