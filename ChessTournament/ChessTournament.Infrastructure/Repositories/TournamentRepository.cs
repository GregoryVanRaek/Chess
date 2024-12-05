using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Domain.Models;
using ChessTournament.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChessTournament.Infrastructure.Repositories;

public class TournamentRepository :ITournamentRepository
{
    private readonly DbContextChessTournament _context;

    public TournamentRepository(DbContextChessTournament context)
    {
        this._context = context;
    }
    
    public IAsyncEnumerable<Tournament> GetAllAsync()
    {
        return this._context.Tournaments.AsAsyncEnumerable();
    }

    public async Task<Tournament?> GetOneByIdAsync(int key)
    {
        return await _context.Tournaments.SingleOrDefaultAsync(m => m.Id == key);
    }

    public async Task<Tournament> CreateAsync(Tournament entity)
    {
        Tournament insert = _context.Tournaments.Add(entity).Entity;
        await _context.SaveChangesAsync();
        return insert;
    }

    public Task<Tournament> UpdateAsync(Tournament entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Tournament entity)
    {
        Tournament? toDelete = await GetOneByIdAsync((int)entity.Id);

        if (toDelete != null)
        {
            _context.Tournaments.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }
        
        return false;
    }
}