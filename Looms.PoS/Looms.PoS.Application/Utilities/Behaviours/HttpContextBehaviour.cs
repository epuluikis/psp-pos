using Looms.PoS.Application.Abstracts;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class HttpContextBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : LoomsHttpRequest, IRequest<TResponse>
{
    private readonly ITokenService _tokenService;

    public HttpContextBehaviour(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not AuthRequest)
        {
            StoreTokenData(request.Request);
        }

        if (request is not GlobalLoomsHttpRequest)
        {
            StoreHeaderData(request.Request);
        }

        return next();
    }

    private void StoreTokenData(HttpRequest request)
    {
        request.Headers.TryGetValue(TokenConstants.TokenHeader, out var token);
        var tokenString = token[0]!;
        var tokenData = _tokenService.GetTokenData(tokenString[TokenConstants.TokenPrefix.Length..]);

        request.HttpContext.Items[TokenConstants.UserIdClaim] = tokenData.UserId;
        request.HttpContext.Items[TokenConstants.UserRoleClaim] = tokenData.UserRole;
        request.HttpContext.Items[TokenConstants.BusinessIdClaim] = tokenData.BusinessId;
    }

    private void StoreHeaderData(HttpRequest request)
    {
        request.Headers.TryGetValue(HeaderConstants.BusinessIdHeader, out var businessId);

        request.HttpContext.Items[HeaderConstants.BusinessIdHeader] = businessId[0];
    }
}
