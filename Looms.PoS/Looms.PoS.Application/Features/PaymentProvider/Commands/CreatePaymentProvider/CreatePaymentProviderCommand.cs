using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;

public record CreatePaymentProviderCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreatePaymentProviderCommand(HttpRequest request) : base(request)
    {
    }
}
