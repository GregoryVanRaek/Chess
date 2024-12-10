using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChessTournament.Domain.Const;
using ChessTournament.Domain.Enum;
using ChessTournament.Domain.Exception;
using Isopoh.Cryptography.Argon2;

namespace ChessTournament.Applications.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
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
            
            entity.Password = Argon2.Hash(entity.Password);

            return await _memberRepository.CreateAsync(entity);
        }
        catch (DBException e)
        {
            throw new DBException("Error while creating a new member");
        }
    }
    
    public async Task<Member> Login(string username, string password)
    {
        Member? member = await _memberRepository.GetOneByEmailOrUsernameAsync(username);
        
        if (member is not null && Argon2.Verify(member.Password, password))
            return member;
        
        return null;
    }

    #region Private methods

    private async Task CheckUniqueAsync(Member entity)
    {
        Member? member = await _memberRepository.GetOneByEmailOrUsernameAsync(entity.Username);

        if (member != null && (member.Mail == entity.Mail || member.Username == entity.Username))
            throw new AlreadyExistException("Member already exists");
    }
    
    public async IAsyncEnumerable<Member> CheckParticipation(Tournament tournament)
    {
        if (CheckTournamentValidity(tournament))
        {
            await foreach (Member member in GetAllAsync())
            {
                if (!tournament.Members.Contains(member))
                {
                    int age = CheckMemberAge(member, tournament);
    
                    if (CheckCategory(age, tournament) 
                        && CheckElo(member.Elo, tournament)
                        && CheckGender(member.Gender, tournament))
                    {
                        Console.WriteLine(member);
                        yield return member;
                    }
                }
            }
        }
    }
    
    public bool CheckOneParticipation(Tournament tournament, Member member)
    {
        if (CheckTournamentValidity(tournament))
        {
            if (!tournament.Members.Contains(member))
            {
                int age = CheckMemberAge(member, tournament);

                if (CheckCategory(age, tournament) 
                    && CheckElo(member.Elo, tournament)
                    && CheckGender(member.Gender, tournament))
                {
                    Console.WriteLine(member);
                    return true;
                }
            }
        }

        return false;
    }
    
    private bool CheckTournamentValidity(Tournament tournament)
    {
        return    tournament.State == TournamentState.WaitingForPlayer
               && tournament.RegistrationEndDate.Date > DateTime.Now.Date
               && tournament.Members.Count < tournament.PlayerMax;
    }

    private bool CheckCategory(int age, Tournament tournament)
    {
        var juniorCategory = tournament.Categories.Any(c => c.Name == CategoryEnum.Junior);
        var seniorCategory = tournament.Categories.Any(c => c.Name == CategoryEnum.Senior);
        var veteranCategory = tournament.Categories.Any(c => c.Name == CategoryEnum.Veteran);
        
        return (age < 18 && juniorCategory) ||
               (age >= 18 && age < 60 && seniorCategory) ||
               (age >= 60 && veteranCategory);
        
    }

    private int CheckMemberAge(Member member, Tournament tournament)
    {
        DateTime date = tournament.RegistrationEndDate;
        int age = date.Year - member.Birthday.Year;
    
        if (member.Birthday.Date > date.AddYears(-age)) 
            age--;

        return age;
    }

    private bool CheckElo(int? memberElo, Tournament tournament)
    {
        return memberElo >= tournament.EloMin && memberElo <= tournament.EloMax;
    }

    private bool CheckGender(Gender gender, Tournament tournament)
    {
        return (tournament.WomenOnly && (gender == Gender.Female || gender == Gender.Other)
                || !tournament.WomenOnly && gender == Gender.Male);
    }
    
    #endregion
    

    
    
}
