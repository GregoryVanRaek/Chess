using ChessTournament.API.DTO.Member;
using ChessTournament.Domain.Models;

namespace ChessTournament.API.Mappers;

public static class MemberMapper
{
    public static MemberViewDTO ToListDTO(this Member member)
    {
        return new MemberViewDTO()
        {
            Id = member.Id,
            Username = member.Username,
            Mail = member.Mail,
            Birthday = member.Birthday,
            Gender = member.Gender,
            Elo = member.Elo,
            Role = member.Role
        };
    }

    public static MemberDTO ToDTO(this Member member)
    {
        return new MemberDTO()
        {
            Id = member.Id,
            Username = member.Username,
            Mail = member.Mail,
            Birthday = member.Birthday,
            Gender = member.Gender,
            Elo = member.Elo,
            Role = member.Role
        };
    }
    
}