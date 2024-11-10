using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Abstracts;

public abstract record LoomsHttpRequest
{
    public HttpRequest Request { get; init; }

    protected LoomsHttpRequest(HttpRequest request)
    {
        Request = request;
    }
}
