using Looms.PoS.Application.Constants;
using Looms.PoS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Helpers;

public static class HttpContextHelper
{
    public static string GetUserId(HttpRequest request)
    {
        return (string)request.HttpContext.Items[TokenConstants.UserIdClaim]!;
    }

    public static UserRole GetUserRole(HttpRequest request)
    {
        return (UserRole)request.HttpContext.Items[TokenConstants.UserRoleClaim]!;
    }

    public static string GetTokenBusinessId(HttpRequest request)
    {
        return (string)request.HttpContext.Items[TokenConstants.BusinessIdClaim]!;
    }

    public static string GetHeaderBusinessId(HttpRequest request)
    {
        return (string)request.HttpContext.Items[HeaderConstants.BusinessIdHeader]!;
    }
}
