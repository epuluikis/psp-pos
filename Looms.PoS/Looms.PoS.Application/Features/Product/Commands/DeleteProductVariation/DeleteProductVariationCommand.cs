using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProductVariation;

public record DeleteProductVariationCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }
    
    public DeleteProductVariationCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
