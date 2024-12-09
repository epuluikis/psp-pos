﻿using Looms.PoS.Application.Abstracts;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Exceptions;
using MediatR;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : LoomsHttpRequest, IRequest<TResponse>
{
    private readonly ITokenService _tokenService;
    private const string TokenPrefix = "Bearer ";

    public AuthenticationBehavior(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is AuthRequest)
        {
            return await next();
        }

        if (!request.Request.Headers.TryGetValue(TokenConstants.TokenHeader, out var token)
            || token.Count == 0)
        {
            throw new LoomsUnauthorizedException("Token not provided");
        }

        var tokenString = token[0]!;
        if (!tokenString.StartsWith(TokenPrefix) || !_tokenService.IsTokenValid(tokenString[TokenPrefix.Length..]))
        {
            throw new LoomsUnauthorizedException("Invalid token provided");
        }

        return await next();
    }
}
