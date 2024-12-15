using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProduct;

public record UpdateProductCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }
    
    public UpdateProductCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
