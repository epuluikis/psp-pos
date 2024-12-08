using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProduct;

public record DeleteProductCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }
    
    public DeleteProductCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
