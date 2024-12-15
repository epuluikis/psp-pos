using Looms.PoS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IPermissionService
{
    void CheckPermissions(HttpRequest request, IEnumerable<UserRole> requiredRoles);
}
