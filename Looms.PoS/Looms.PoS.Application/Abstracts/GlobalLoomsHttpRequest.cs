using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Abstracts;

public record GlobalLoomsHttpRequest : LoomsHttpRequest
{
    public GlobalLoomsHttpRequest(HttpRequest request) : base(request)
    {
    }
}
