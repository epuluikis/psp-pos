using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Abstracts;

public abstract record WebhookRequest : GlobalLoomsHttpRequest
{
    protected WebhookRequest(HttpRequest request) : base(request)
    {
    }
}
