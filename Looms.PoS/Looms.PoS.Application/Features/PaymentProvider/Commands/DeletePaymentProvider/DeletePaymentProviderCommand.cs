using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.DeletePaymentProvider;

public record DeletePaymentProviderCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeletePaymentProviderCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
