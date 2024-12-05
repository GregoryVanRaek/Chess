using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChessTournament.Domain.Const;
using ChessTournament.Domain.Enum;
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
            if (entity.Elo is null || entity.Elo == 0)
                entity.Elo = MemberConst.DEFAULT_ELO;
            
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

    public async Task<int> CheckMemberAge(Member entity, DateTime date)
    {
        Member member = await _memberRepository.GetOneByIdAsync((int)entity.Id);
        int age = date.Year - member.Birthday.Year;
        
        if (member.Birthday.Date > date.AddYears(-age)) 
            age--;

        return age;
    }

    public async IAsyncEnumerable<Member> CheckParticipation(Tournament tournament)
    {
        var memberConcerned = GetAllAsync().ToBlockingEnumerable()
            .Where(m => m.Elo <= tournament.EloMax && m.Elo >= tournament.EloMin);
        
        if (tournament.WomenOnly)
            memberConcerned = memberConcerned.Where(m => m.Gender != Gender.Male);
        
        foreach (var member in memberConcerned)
        {
            int age = await CheckMemberAge(member, tournament.RegistrationEndDate);

            if (!((age < 18 && tournament.Categories.Any(c => c.Name == CategoryEnum.Junior))
                || (age >= 18 && age < 60 && tournament.Categories.Any(c => c.Name ==CategoryEnum.Senior))
                || (age >= 60 && tournament.Categories.Any(c => c.Name == CategoryEnum.Veteran))))
            {
                yield return member;
            }
        }
    }
    
}
