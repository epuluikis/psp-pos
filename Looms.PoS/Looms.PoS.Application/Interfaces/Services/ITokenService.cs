using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface ITokenService
{
    string CreateToken(UserDao userDao);
    void ValidateToken(string token);
}
