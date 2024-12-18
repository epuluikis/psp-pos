using Looms.PoS.Application.Helpers;
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
        var (businessId, requiredBusinessId, userRole) = GetRequestData(request);

        ThrowIfInsufficient(!requiredRoles.Contains(userRole));

        ThrowIfInsufficient(userRole != UserRole.SuperAdmin
            && !requiredBusinessId.Equals(businessId, StringComparison.InvariantCultureIgnoreCase));
    }

    private static (string BusinessId, string RequiredBusinessId, UserRole UserRole) GetRequestData(HttpRequest request)
    {
        var businessId = HttpContextHelper.GetHeaderBusinessId(request);
        var requiredBusinessId = HttpContextHelper.GetTokenBusinessId(request);
        var userRole = HttpContextHelper.GetUserRole(request);

        return (businessId, requiredBusinessId, userRole);
    }

    private static void ThrowIfInsufficient(bool isInsufficient)
    {
        if (isInsufficient)
        {
            throw new LoomsForbiddenException("Insufficient permissions");
        }
    }
}
