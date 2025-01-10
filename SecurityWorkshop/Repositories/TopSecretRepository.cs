using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SecurityWorkshop.Data;
using SecurityWorkshop.Models;
using SecurityWorkshop.Repositories.Interfaces;

namespace SecurityWorkshop.Repositories;

public class TopSecretRepository : ITopSecretRepository
{
    private readonly DatabaseContext _context;

    public TopSecretRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<TopSecret>> getAll()
    {
        return await _context.TopSecret.ToListAsync();
    }

    public async Task<bool> addEntry(TopSecret entry)
    {
        await _context.TopSecret.AddAsync(entry);
        int numberOfChanges = await _context.SaveChangesAsync();
        return numberOfChanges == 1;
    }
}