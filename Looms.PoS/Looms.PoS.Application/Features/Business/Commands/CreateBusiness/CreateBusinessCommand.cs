using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public record CreateBusinessCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateBusinessCommand(HttpRequest request) : base(request)
    {
    }
}
