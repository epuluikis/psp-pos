using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Services;

public interface ITokenService
{
    LoginResponse CreateToken(UserDao userDao);
    (string BusinessId, UserRole UserRole) GetTokenData(string token);
    bool IsTokenValid(string token);
}
