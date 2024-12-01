using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProductVariation;

public record CreateProductVariationCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateProductVariationCommand(HttpRequest request) : base(request)
    {
    }
}
