using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Domain.Enum;
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
        return this._context.Tournaments.Include(c => c.Categories)
                                        .Include(m => m.Members)
                                        .AsAsyncEnumerable();
    }

    public async Task<Tournament?> GetOneByIdAsync(int key)
    {
        return await _context.Tournaments.Include(c => c.Categories).SingleOrDefaultAsync(m => m.Id == key);
    }

    public IEnumerable<Tournament> GetSomeTournament(int number)
    {
        
        IEnumerable<Tournament> tournaments =  this._context.Tournaments
            .Include(c => c.Categories)
            .Include(m => m.Members)
            .Where(t => t.State == TournamentState.WaitingForPlayer) 
            .OrderByDescending(t => t.UpdateDate)  
            .Take(number)
            .AsEnumerable();

        return tournaments;
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
        if (entity == null || entity.Id == null)
            throw new ArgumentNullException(nameof(entity), "The tournament to delete cannot be null.");
        
        Tournament? toDelete = await GetOneByIdAsync((int)entity.Id);
        
        if (toDelete == null)
            throw new Exception("The member to delete doesn't exist");
        
        _context.Tournaments.Remove(toDelete);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Category>> MapCategoriesAsync(List<CategoryEnum> categoryEnums)
    {
        var categories = new List<Category>();

        foreach (var categoryEnum in categoryEnums)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == categoryEnum);

            if (existingCategory != null)
                categories.Add(existingCategory);
            else
                throw new Exception($"Category {categoryEnum} does not exist.");
        }

        return categories;
    }

    public Task<Tournament> CreateAsync(Tournament entity, List<CategoryEnum> categoryEnums)
    {
        throw new NotImplementedException();
    }
}