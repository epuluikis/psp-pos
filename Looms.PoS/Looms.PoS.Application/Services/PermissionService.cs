using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Services;

public class PermissionService : IPermissionService
{
    private readonly ITokenService _tokenService;

    public PermissionService(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public void CheckPermissions(HttpRequest request, IEnumerable<UserRole> requiredRoles)
    {
        var businessId = GetBusinessId(request);
        var (requiredBusinessId, userRole) = _tokenService.GetTokenData(GetToken(request));

        ThrowIfInsufficient(!requiredRoles.Contains(userRole));

        ThrowIfInsufficient(userRole != UserRole.SuperAdmin
            && !requiredBusinessId.Equals(businessId, StringComparison.InvariantCultureIgnoreCase));
    }

    private static string GetToken(HttpRequest request)
    {
        var token = request.Headers[TokenConstants.TokenHeader][0]!;
        return token[TokenConstants.TokenPrefix.Length..];
    }

    private static string GetBusinessId(HttpRequest request)
    {
        return request.Headers[HeaderConstants.BusinessIdHeader]!;
    }

    private static void ThrowIfInsufficient(bool isInsufficient)
    {
        if (isInsufficient)
        {
            throw new LoomsUnauthorizedException("Insufficient permissions");
        }
    }
}
