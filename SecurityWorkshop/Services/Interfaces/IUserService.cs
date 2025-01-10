namespace SecurityWorkshop.Services.Interfaces;

public interface IUserService
{
    public Task<string?> getToken(string email, string password);
    public Task<bool> resetPassword(int pin, string newPassword);
    public Task<bool> createUser(string email, string password);
    public Task<int?> getResetPin(string email);
}