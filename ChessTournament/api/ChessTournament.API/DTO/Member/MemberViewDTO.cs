using ChessTournament.API.DTO.Tournament;
using ChessTournament.Domain.Enum;

namespace ChessTournament.API.DTO.Member;

public class MemberViewDTO
{
    public int? Id { get; set; }
    public required string Username { get; set; }
    public required string Mail { get; set; }
    public required DateTime Birthday { get; set; }
    public required Gender Gender { get; set; }
    public int? Elo { get; set; }
}