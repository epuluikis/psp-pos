using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

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

    public async Task<IEnumerable<UserDao>> GetAllAsync()
    {
        return await _context.Users.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<UserDao> GetAsync(Guid id)
    {
        var userDao = await _context.Users.FindAsync(id);

        if (userDao is null || userDao.IsDeleted)
        {
            throw new LoomsNotFoundException("User not found");
        }

        return userDao;
    }

    public async Task<UserDao> GetByEmailAsync(string email)
    {
        var userDao = _context.ChangeTracker.Entries<UserDao>().FirstOrDefault(x => x.Entity.Email == email && !x.Entity.IsDeleted)?.Entity;

        if (userDao is null)
        {
            userDao = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted);
        }

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
