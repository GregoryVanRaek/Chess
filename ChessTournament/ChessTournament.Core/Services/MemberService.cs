using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

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

    public async Task<List<Member>> GetAllAsync()
    {
        try
        {
            return await _memberRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    public async Task<Member?> GetOneByIdAsync(int key)
    {
        try
        {
            return await _memberRepository.GetOneByIdAsync(key);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    public async Task<Member> CreateAsync(Member entity)
    {
        try
        {
            await CheckUniqueAsync(entity);

            entity.Password = _passwordService.HashPassword(entity.Password, entity.Mail);

            return await _memberRepository.CreateAsync(entity);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    public async Task<Member> UpdateAsync(Member entity)
    {
        try
        {
            return await _memberRepository.UpdateAsync(entity);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    public async Task<bool> DeleteAsync(Member entity)
    {
        try
        {
            return await _memberRepository.DeleteAsync(entity);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error: " + e.Message);
        }
    }

    private async Task CheckUniqueAsync(Member entity)
    {
        Member? member = await GetOneByIdAsync(entity.Id);

        if (member != null && (member.Mail == entity.Mail || member.Username == entity.Username))
            throw new Exception("Member already exists");
    }
}
