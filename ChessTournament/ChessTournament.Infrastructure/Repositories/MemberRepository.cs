using ChessTournament.Domain.Models;
using ChessTournament.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ChessTournament.Applications.Interfaces.Repository;

namespace ChessTournament.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly DbContextChessTournament _context;

    public MemberRepository(DbContextChessTournament context)
    {
        _context = context;
    }

    public async Task<List<Member>> GetAllAsync()
    {
        try
        {
            return await _context.Members.ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error during getall request in database: " + e.Message);
        }
    }

    public async Task<Member?> GetOneByIdAsync(int key)
    {
        try
        {
            return await _context.Members.SingleOrDefaultAsync(m => m.Id == key);
        }
        catch (Exception e)
        {
            throw new Exception("Error during getbyid request in database: " + e.Message);
        }
    }

    public async Task<Member?> GetOneByEmailOrUsernameAsync(string mail, string username)
    {
        try
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Username == username || m.Mail == mail);
        }
        catch (Exception e)
        {
            throw new Exception("Error during get by email or username request in database: " + e.Message);
        }
    }

    public async Task<Member> CreateAsync(Member entity)
    {
        try
        {
            var insert = _context.Members.Add(entity).Entity;
            await _context.SaveChangesAsync();
            return insert;
        }
        catch (Exception e)
        {
            throw new Exception("Error during creation in database: " + e.Message);
        }
    }

    public async Task<Member> UpdateAsync(Member entity)
    {
        try
        {
            Member? entityToUpdate = await _context.Members.FirstOrDefaultAsync(d => d.Id == entity.Id);

            if (entityToUpdate == null)
                throw new Exception("Entity not found");

            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return entityToUpdate;
        }
        catch (Exception e)
        {
            throw new Exception("Error during the update in database: " + e.Message);
        }
    }

    public async Task<bool> DeleteAsync(Member entity)
    {
        try
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
        catch (Exception e)
        {
            throw new Exception("Error during delete in database: " + e.Message);
        }
    }
}
