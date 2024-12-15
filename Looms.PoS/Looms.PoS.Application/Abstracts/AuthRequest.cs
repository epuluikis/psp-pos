using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Abstracts;

public abstract record AuthRequest : GlobalLoomsHttpRequest
{
    protected AuthRequest(HttpRequest request) : base(request)
    {
    }
}
