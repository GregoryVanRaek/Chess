using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Exception;
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
                                        .Include(t => t.Games)
                                        .ThenInclude(g => g.GameMembers)
                                        .ThenInclude(gm => gm.Member) 
                                        .AsAsyncEnumerable();
    }

    public async Task<Tournament?> GetOneByIdAsync(int key)
    {
        return await _context.Tournaments.Include(c => c.Categories)
                                         .Include(m => m.Members)
                                         .Include(t => t.Games)
                                         .ThenInclude(g => g.GameMembers)
                                         .ThenInclude(gm => gm.Member)
                                         .SingleOrDefaultAsync(m => m.Id == key);
    }

    public IEnumerable<Tournament> GetSomeTournament(int number)
    {
        
        IEnumerable<Tournament> tournaments =  this._context.Tournaments
            .Include(c => c.Categories)
            .Include(m => m.Members)
            .Include(t => t.Games)
            .ThenInclude(g => g.GameMembers)
            .ThenInclude(gm => gm.Member)
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
    
    public async Task<int> AddPlayers(Tournament tournament, Member member)
    {
        Tournament? t = await this.GetOneByIdAsync((int)tournament.Id);
        t.Members.Add(member);
   
        return await this._context.SaveChangesAsync();
    }
    
    public async Task<int> RemovePlayers(Tournament tournament, Member member)
    {
        Tournament? t = await this.GetOneByIdAsync((int)tournament.Id);
        t.Members.Remove(member);
   
        return await this._context.SaveChangesAsync();
    }

    public Task<Tournament> CreateAsync(Tournament entity, List<CategoryEnum> categoryEnums)
    {
        throw new NotImplementedException();
    }

    public async Task<Tournament> StartTournament(Tournament entity)
    {
        Tournament? t = await this.GetOneByIdAsync((int)entity.Id);
        
        _context.Entry(t).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return t;
    }
    
}