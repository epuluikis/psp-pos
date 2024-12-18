using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDao> CreateAsync(UserDao userDao)
    {
        userDao = _context.CreateProxy<UserDao>(userDao);

        var entityEntry = await _context.AddAsync(userDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<UserDao>> GetAllByBusinessAsync(Guid businessId)
    {
        return await _context.Users.Where(x => x.BusinessId == businessId && !x.IsDeleted).ToListAsync();
    }

    public async Task<UserDao> GetByBusinessAsync(Guid id, Guid businessId)
    {
        var userDao = _context.ChangeTracker.Entries<UserDao>().FirstOrDefault(x => x.Entity.Id == id && x.Entity.BusinessId == businessId)?.Entity;

        userDao ??= await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.BusinessId == businessId);

        if (userDao is null || userDao.IsDeleted)
        {
            throw new LoomsNotFoundException("User not found");
        }

        return userDao;
    }

    public async Task<UserDao> GetByEmailAsync(string email)
    {
        var userDao = _context.ChangeTracker.Entries<UserDao>().FirstOrDefault(x => x.Entity.Email == email && !x.Entity.IsDeleted)?.Entity;

        userDao ??= await _context.Users.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);

        if (userDao is null || userDao.IsDeleted)
        {
            throw new LoomsNotFoundException("User not found");
        }

        return userDao;
    }

    public async Task<UserDao> UpdateAsync(UserDao userDao)
    {
        await RemoveAsync(userDao.Id);
        _context.Users.Update(userDao);
        await _context.SaveChangesAsync();

        return userDao;
    }

    public async Task<bool> ExistsWithEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email.ToLower() == email.ToLower() && !user.IsDeleted);
    }

    private async Task RemoveAsync(Guid id)
    {
        var userDao = await _context.Users.FindAsync(id);
        _context.Users.Remove(userDao!);
    }
}
