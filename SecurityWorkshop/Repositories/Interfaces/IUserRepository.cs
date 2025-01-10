using SecurityWorkshop.Data;
using SecurityWorkshop.Models;

namespace SecurityWorkshop.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUser(string email, string password);
    public Task<bool> ResetPassword(User user);
    public Task<bool> createUser(User user);
    public Task<User?> getWithEmail(string email);
    public Task<bool> isUniqueEmail(string email);
    public Task<User?> getUserWithResetPin(int pin);
}