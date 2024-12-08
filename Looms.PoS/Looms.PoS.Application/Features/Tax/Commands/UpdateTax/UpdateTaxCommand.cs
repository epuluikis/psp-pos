using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.UpdateTax;

public record UpdateTaxCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateTaxCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
