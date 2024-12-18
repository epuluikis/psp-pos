using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Dtos;
using Looms.PoS.Application.Models.Responses.Auth;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class AuthModelsResolver : IAuthModelsResolver
{
    public LoginResponse GetResponse(string token, DateTime expires, TokenDataDto tokenDataDto)
    {
        return new()
        {
            Token = token,
            Expires = expires,
            UserId = Guid.Parse(tokenDataDto.UserId),
            Role = tokenDataDto.UserRole,
            BusinessId = Guid.Parse(tokenDataDto.BusinessId)
        };
    }
}
