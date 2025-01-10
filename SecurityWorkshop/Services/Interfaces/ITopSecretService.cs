using SecurityWorkshop.Models;

namespace SecurityWorkshop.Services.Interfaces;

public interface ITopSecretService
{
    public Task<List<TopSecret>> getAll();
    public Task<bool> addEntry(string codeName, string realName);
}