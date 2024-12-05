using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChessTournament.Domain.Exception;

namespace ChessTournament.Applications.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordService _passwordService;

    public MemberService(IMemberRepository memberRepository, IPasswordService passwordService)
    {
        _memberRepository = memberRepository;
        _passwordService = passwordService;
    }

    public IAsyncEnumerable<Member> GetAllAsync()
    {
        try
        {
            return _memberRepository.GetAllAsync();
        }
        catch (DBException e)
        {
            throw new DBException("Error while getting all members ");
        }
    }

    public async Task<Member?> GetOneByIdAsync(int key)
    {
        try
        {
            return await _memberRepository.GetOneByIdAsync(key);
        }
        catch (DBException e)
        {
            throw new DBException("Error while getting the member ");
        }
    }

    public async Task<Member> CreateAsync(Member entity)
    {
        await CheckUniqueAsync(entity);
        
        try
        {
            if (entity.Elo == 0)
                entity.Elo = 1200;
            
            entity.Password = _passwordService.HashPassword(entity.Password, entity.Mail);

            return await _memberRepository.CreateAsync(entity);
        }
        catch (DBException e)
        {
            throw new DBException("Error while creating a new member");
        }
    }
    
    private async Task CheckUniqueAsync(Member entity)
    {
        Member? member = await _memberRepository.GetOneByEmailOrUsernameAsync(entity.Mail, entity.Username);

        if (member != null && (member.Mail == entity.Mail || member.Username == entity.Username))
            throw new AlreadyExistException("Member already exists");
    }
}
