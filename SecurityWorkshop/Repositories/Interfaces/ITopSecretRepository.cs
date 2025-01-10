using SecurityWorkshop.Models;

namespace SecurityWorkshop.Repositories.Interfaces;

public interface ITopSecretRepository
{
    public Task<List<TopSecret>> getAll();
    public Task<bool> addEntry(TopSecret entry);
}