using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.UpdatePaymentProvider;

public record UpdatePaymentProviderCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdatePaymentProviderCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
