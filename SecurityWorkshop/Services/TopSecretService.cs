using SecurityWorkshop.Models;
using SecurityWorkshop.Repositories.Interfaces;
using SecurityWorkshop.Services.Interfaces;

namespace SecurityWorkshop.Services;

public class TopSecretService : ITopSecretService
{
    private readonly ITopSecretRepository _topSecretRepository;

    public TopSecretService(ITopSecretRepository topSecretRepository)
    {
        _topSecretRepository = topSecretRepository;
    }

    public async Task<List<TopSecret>> getAll()
    {
        return await _topSecretRepository.getAll();
    }

    public async Task<bool> addEntry(string codeName, string realName)
    {
        TopSecret entry = new TopSecret
        {
            codeName = codeName,
            realName = realName
        };

        return await _topSecretRepository.addEntry(entry);
    }
}