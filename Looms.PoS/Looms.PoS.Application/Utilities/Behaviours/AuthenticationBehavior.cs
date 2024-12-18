using Looms.PoS.Application.Abstracts;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : LoomsHttpRequest, IRequest<TResponse>
{
    private readonly ITokenService _tokenService;

    public AuthenticationBehavior(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is AuthRequest or WebhookRequest)
        {
            return await next();
        }

        if (!request.Request.Headers.TryGetValue(TokenConstants.TokenHeader, out var token)
         || token.Count == 0)
        {
            throw new LoomsUnauthorizedException("Token not provided");
        }

        var tokenString = token[0]!;

        if (!tokenString.StartsWith(TokenConstants.TokenPrefix)
         || !_tokenService.IsTokenValid(tokenString[TokenConstants.TokenPrefix.Length..]))
        {
            throw new LoomsUnauthorizedException("Invalid token provided");
        }

        StoreTokenData(request.Request, tokenString[TokenConstants.TokenPrefix.Length..]);
        return await next();
    }

    private void StoreTokenData(HttpRequest request, string token)
    {
        var tokenData = _tokenService.GetTokenData(token);

        request.HttpContext.Items[TokenConstants.UserIdClaim] = tokenData.UserId;
        request.HttpContext.Items[TokenConstants.UserRoleClaim] = tokenData.UserRole;
        request.HttpContext.Items[TokenConstants.BusinessIdClaim] = tokenData.BusinessId;
    }
}
