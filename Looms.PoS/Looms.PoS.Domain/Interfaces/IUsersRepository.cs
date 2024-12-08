using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IUsersRepository
{
    Task<UserDao> CreateAsync(UserDao userDao);
    Task<IEnumerable<UserDao>> GetAllAsync();
    Task<UserDao> GetAsync(Guid id);
    Task<UserDao> GetByEmailAsync(string email);
    Task<UserDao> UpdateAsync(UserDao userDao);
    Task<bool> ExistsWithEmail(string email);
}
