using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class AuthModelsResolver : IAuthModelsResolver
{
    public LoginResponse GetResponse(string token, DateTime expires, UserRole role, Guid businessId)
    {
        return new()
        {
            Token = token,
            Expires = expires,
            Role = role,
            BusinessId = businessId
        };
    }
}
