using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public record CreateProductCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateProductCommand(HttpRequest request) : base(request)
    {
        //TODO: idk where to check if this has a body, but from what i understand it doesnt 
    }
}
