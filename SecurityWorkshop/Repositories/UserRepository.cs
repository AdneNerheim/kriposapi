using System.ComponentModel.DataAnnotations;
using SecurityWorkshop.Data;
using SecurityWorkshop.Models;
using SecurityWorkshop.Utils;
using Microsoft.EntityFrameworkCore;
using SecurityWorkshop.Repositories.Interfaces;

namespace SecurityWorkshop.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    private readonly ILogger _logger;

    public UserRepository(DatabaseContext context, ILogger<UserRepository> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<User?> getUserWithResetPin(int pin)
    {
        List<User> users = await _context.User.Where(x => x.resetPin == pin).ToListAsync();
        if (users.Count == 1)
            return users[0];
        return null;
    }

    public async Task<bool> isUniqueEmail(string email)
    {
        List<User> user = await _context.User.Where(x => x.email == email).ToListAsync();
        return user.Count == 0;
    }

    public async Task<User?> getWithEmail(string email)
    {
        List<User> user = await _context.User.FromSqlRaw($"SELECT * FROM dbo.users WHERE Email = {email}").ToListAsync();
        if (user.Count == 1)
            return user[0];
        return null;
    }

    public async Task<List<User>> getUserPin(string email)
    {
        var pin = await _context.User.FromSqlRaw($"SELECT * FROM dbo.users WHERE email = {email}").ToListAsync();
        return pin;
    }

    public async Task<User?> GetUser(string email, string password)
    {
        _logger.LogError($"SELECT * FROM dbo.users WHERE Email = {email} AND Password = {password}");
        List<User> users = await _context.User
            .FromSqlRaw($"SELECT * FROM dbo.users WHERE Email = {email} AND Password = {password}")
            .ToListAsync();

        if (users.Count == 0)
        {
            return null;
        }
        return users[0];
    }

    public async Task<bool> createUser(User user)
    {
        await _context.AddAsync(user);
        int changes = await _context.SaveChangesAsync();
        return changes == 1;
    }

    public async Task<bool> ResetPassword(User user)
    {
        //Make sure the user object is tracked by the context
        _context.User.Update(user);
        //Save changes to databse - returns number of objects changed
        int numberOfChanges = await _context.SaveChangesAsync();

        //If one object was updated we return true
        return numberOfChanges == 1;
    }
}