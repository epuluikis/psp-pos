using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.DeleteTax;

public record DeleteTaxCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteTaxCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}