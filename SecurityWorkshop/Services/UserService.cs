using SecurityWorkshop.Models;
using SecurityWorkshop.Repositories.Interfaces;
using SecurityWorkshop.Services.Interfaces;
using SecurityWorkshop.Utils;

namespace SecurityWorkshop.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHelper _passwordHelper;

    public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper)
    {
        _userRepository = userRepository;
        _passwordHelper = passwordHelper;
    }

    public async Task<string?> getToken(string email, string password)
    {
        TokenManager tokenManager = new TokenManager();
        User? user = await _userRepository.getWithEmail(email);
        if (user == null || !_passwordHelper.VerifyPassword(user, user.password, password))
            return null;
        return tokenManager.getToken();
    }

    public async Task<bool> resetPassword(string email, int pin, string newPassword)
    {
        User? user = await _userRepository.getWithEmail(email);
        if (user == null || user.resetPin != pin)
            return false;

        user.password = _passwordHelper.HashPassword(user, newPassword);
        return await _userRepository.ResetPassword(user);
    }

    public async Task<bool> createUser(string email, string password)
    {
        if (!await _userRepository.isUniqueEmail(email))
            return false;
        
        User user = new User();
        user.email = email;
        user.password = _passwordHelper.HashPassword(user, password);
        user.resetPin = GenerateRandomNo();
        return await _userRepository.createUser(user);
    }

    public async Task<int?> getResetPin(string email)
    {
        var user = await _userRepository.getUserPin(email);
        if (user.Count != 1)
            return null;
        return user[0].resetPin;
    }
    
    private int GenerateRandomNo()
    {
        int _min = 0000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
}