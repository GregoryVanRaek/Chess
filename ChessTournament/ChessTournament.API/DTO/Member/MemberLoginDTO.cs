using System.ComponentModel.DataAnnotations;

namespace ChessTournament.API.DTO.Member;

public class MemberLoginDTO
{
    public required string Username;
    public required string Password;
}