using Looms.PoS.Application.Abstracts;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Features.Auth.Commands.Login;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Exceptions;
using MediatR;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : LoomsHttpRequest, IRequest<TResponse>
{
    private readonly ITokenService _tokenService;

    public AuthorizationBehavior(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is LoginCommand)
        {
            return await next();
        }

        if (!request.Request.Headers.TryGetValue(TokenConstants.TokenHeader, out var token)
            || token.Count == 0)
        {
            throw new LoomsUnauthorizedException("Token not provided");
        }

        try
        {
            _tokenService.ValidateToken(token[0]!);
        }
        catch (Exception)
        {
            throw new LoomsUnauthorizedException("Invalid token provided");
        }

        return await next();
    }
}
