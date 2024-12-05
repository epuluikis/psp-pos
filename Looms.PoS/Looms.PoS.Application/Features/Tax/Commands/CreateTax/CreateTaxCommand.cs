using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.CreateTax;

public record CreateTaxCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateTaxCommand(HttpRequest request) : base(request)
    {
    }
}
