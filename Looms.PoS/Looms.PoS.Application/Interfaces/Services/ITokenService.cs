using Looms.PoS.Application.Models.Dtos;
using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface ITokenService
{
    LoginResponse CreateToken(UserDao userDao);
    TokenDataDto GetTokenData(string token);
    bool IsTokenValid(string token);
}
