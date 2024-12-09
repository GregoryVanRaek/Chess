using ChessTournament.Domain.Models;
using ChessTournament.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Domain.Exception;

namespace ChessTournament.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly DbContextChessTournament _context;

    public MemberRepository(DbContextChessTournament context)
    {
        _context = context;
    }

    public IAsyncEnumerable<Member> GetAllAsync()
    {
        return this._context.Members.Include(m => m.Tournaments)
                                    .Include(m => m.GameMembers)                        
                                    .AsAsyncEnumerable();
    }

    public async Task<Member?> GetOneByIdAsync(int key)
    {
        return await _context.Members.Include(m => m.Tournaments)
                                     .Include(m => m.GameMembers)                         
                                     .FirstOrDefaultAsync(m => m.Id == key);
    }

    public async Task<Member?> GetOneByEmailOrUsernameAsync(string credential)
    {
        return await _context.Members.Include(m => m.Tournaments)
                                     .Include(m => m.GameMembers)                         
                                     .FirstOrDefaultAsync(m => m.Username == credential || m.Mail == credential);
    }

    public async Task<Member> CreateAsync(Member entity)
    {
        var insert = _context.Members.Add(entity).Entity;
        await _context.SaveChangesAsync();
        return insert;
    }

    public async Task<Member> UpdateAsync(Member entity)
    {
        Member? entityToUpdate = await _context.Members.FirstOrDefaultAsync(d => d.Id == entity.Id);

        if (entityToUpdate == null)
            throw new NotFoundexception("Member not found");

        _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();

        return entityToUpdate;
        
    }

    public async Task<bool> DeleteAsync(Member entity)
    {
        Member? toDelete = await GetOneByIdAsync((int)entity.Id);

        if (toDelete != null)
        {
            _context.Members.Remove(toDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
}
