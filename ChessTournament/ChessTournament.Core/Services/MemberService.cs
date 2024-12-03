using System.Data;
using ChessTournament.Applications.Interfaces.Repository;
using ChessTournament.Applications.Interfaces.Service;
using ChessTournament.Domain.Models;
using Isopoh.Cryptography.Argon2;

namespace ChessTournament.Applications.Services;

public class MemberService :IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IPasswordService _passwordService;

    public MemberService(IMemberRepository memberRepository, IPasswordService passwordService)
    {
        this._memberRepository = memberRepository;
        this._passwordService = passwordService;
    }

    public List<Member> GetAll()
    {
        try
        {
            return this._memberRepository.GetAll();
        }
        catch (Exception e)
        {
            throw new Exception("Member service error" + e.Message);
        }
    }

    public Member? GetOneById(int key)
    {
        try
        {
            return this._memberRepository?.GetOneById(key);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error" + e.Message);
        }
    }

    public Member Create(Member entity)
    {
        try
        {
            this.CheckUnique(entity);

            string salt = _passwordService.GenerateSalt();
            
            entity.Password = _passwordService.HashPassword(entity.Password);
            
            return this._memberRepository.Create(entity);
        }
        catch (Exception e)
        {
            throw new Exception("Member service error" + e.Message);
        }
    }


    public Member Update(Member entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Member entity)
    {
        throw new NotImplementedException();
    }

    private void CheckUnique(Member entity)
    {
        Member? member = this.GetOneById(entity.Id);

        if (member is not null && (member.Mail == entity.Mail || member.Username == entity.Username))
            throw new Exception("Member already exists");
    }
    
}