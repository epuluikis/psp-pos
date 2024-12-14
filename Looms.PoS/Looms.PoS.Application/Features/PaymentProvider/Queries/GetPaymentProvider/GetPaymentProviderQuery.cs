using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProvider;

public record GetPaymentProviderQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetPaymentProviderQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
